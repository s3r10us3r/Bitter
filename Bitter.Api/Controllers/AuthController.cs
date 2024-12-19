using Bitter.Api.DataStorages;
using Bitter.Api.Services.Interfaces;
using Bitter.Dal.Interfaces;
using Bitter.Shared.Dtos;
using Bitter.Shared.Models;
using Bitter.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bitter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepo _userRepo;
    private readonly IRegisterRequestValidator _registerRequestValidator;
    private readonly IRegisterRequestService _registerRequestService;
    private readonly LoginAttemptsDataStorage _loginAttemptsDataStorage;
    private readonly IPasswordHasher<object> _passwordHasher;
    private readonly ILoginService _loginService;
    private readonly ITokenService _tokenService;
    
    public AuthController(IRegisterRequestValidator registerRequestValidator, IUserRepo userRepo,
        IRegisterRequestService registerRequestService, LoginAttemptsDataStorage loginAttemptsDataStorage,
        IPasswordHasher<object> passwordHasher, ILoginService loginService, ITokenService tokenService)
    {
        _registerRequestValidator = registerRequestValidator;
        _userRepo = userRepo;
        _registerRequestService = registerRequestService;
        _loginAttemptsDataStorage = loginAttemptsDataStorage;
        _passwordHasher = passwordHasher;
        _loginService = loginService;
        _tokenService = tokenService;
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerRequest)
    {
        if (!_registerRequestValidator.IsRegisterDtoValid(registerRequest, out var message))
        {
            return BadRequest(new { Message = message });
        }
        var userFromMail = await _userRepo.GetOneFromMailAsync(registerRequest.Email);
        var userFromLogin = await _userRepo.GetOneFromLoginAsync(registerRequest.Login);
        if (userFromMail != null)
        {
            return Conflict(new { Message = "Mail already exists" });
        }
        if (userFromLogin != null)
        {
            return Conflict(new { Message = "Login already exists" });
        }
        await _registerRequestService.CreateRequest(registerRequest);
        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("/verifyRegister")]
    public async Task<IActionResult> VerifyRegister([FromQuery] string id)
    {
        if (_registerRequestService.TryGetUserFromRequest(id, out var user))
        {
            await _userRepo.AddOneAsync(user!);
            return Ok();
        }
        return NotFound();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("/login")]
    public async Task<IActionResult> LogIn([FromBody] LoginDto loginDto)
    {
        if (!_loginAttemptsDataStorage.CanLogIn(loginDto.Username))
        {
            return StatusCode(StatusCodes.Status429TooManyRequests,
                new { Message = "Too many attempts! You have to wait before making another one" });
        }
        var user = await _userRepo.GetOneFromLoginAsync(loginDto.Username);
        if (user == null)
        {
            return Unauthorized(new { Message = "Invalid username and/or password" });
        }
        var verificationResult = _passwordHasher.VerifyHashedPassword(null!, user.PasswordHash, loginDto.Password);
        if (verificationResult == PasswordVerificationResult.Success)
        {
            var requestId = await _loginService.CreateLoginRequest(user);
            return Ok(new { LoginRequestId =  requestId});
        }
        return Unauthorized(new { Message = "Invalid username and/or password" });
    }

    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("verifyLogin")]
    public async Task<IActionResult> VerifyLogin([FromBody] VerifyLoginDto dto)
    {
        if (_loginService.VerifyLoginRequest(dto.RequestId, dto.Code, out var userId))
        {
            var user = await _userRepo.GetOneAsync(userId);
            if (user is null)
            {
                return Unauthorized();
            }
            SetLoginJwtCookie(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            await _userRepo.UpdateAsync(user);
            return Ok(new { RefreshToken = refreshToken });
        }

        return Unauthorized();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        var user = await _userRepo.GetOneFromRefreshTokenAsync(refreshToken);
        if (user != null)
        {
            SetLoginJwtCookie(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userRepo.UpdateAsync(user);
            return Ok(new {RefreshToken = refreshToken});
        }

        return Unauthorized();
    }
    
    
    private void SetLoginJwtCookie(User user)
    {
        var jwtoken = _tokenService.GenerateLoginJwt(user);
        Response.Cookies.Append("AuthToken", jwtoken, new CookieOptions
        {
            HttpOnly = true,
            Secure = false, //TRY TO CHANGE IT WHEN HTTPS IS SET UP
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddMinutes(15)
        });
    }
}
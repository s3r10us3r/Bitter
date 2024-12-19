using Bitter.Api.DataStorages.Extensions;
using Bitter.Api.Services.Extensions;
using Bitter.Dal;
using Bitter.Dal.Extensions;
using Bitter.Shared.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BitterDbContext>();
builder.Services.AddRepos();
builder.Services.AddSharedServices();
builder.Services.AddServices();
builder.Services.AddDataStorages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();

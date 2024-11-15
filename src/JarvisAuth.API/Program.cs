using HealthChecks.UI.Client;
using JarvisAuth.API.Configurations;
using JarvisAuth.API.Handlers;
using JarvisAuth.Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using JarvisAuth.Infrastructure.Configurations;
using JarvisAuth.Application.Configurations;
using Microsoft.AspNetCore.Diagnostics;
using JarvisAuth.API;

var builder = WebApplication.CreateBuilder(args);
var connectionStringSqlite = builder.Configuration.GetConnectionString("Sqlite");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddSqliteDbContext(connectionStringSqlite);
builder.Services.AddSqliteHealthCheck(connectionStringSqlite);
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
builder.Services.RepositoriesDependencies();
builder.Services.ServicesDependencies();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.AddSingleton<IExceptionHandler, GlobalExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddAuthorization();
//builder.Host.ConfigureElasticSearch(builder.Configuration); // Comment out the line if the project is running locally.

InitDb.InitializeDatabase(connectionStringSqlite); //Performs the creation of tables in the SQLite database

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/api/healthz", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
app.UseExceptionHandler();

app.Run();
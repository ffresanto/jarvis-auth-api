using HealthChecks.UI.Client;
using JarvisAuth.API.Configurations;
using JarvisAuth.API.Handlers;
using JarvisAuth.Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using JarvisAuth.Infrastructure.Configurations;
using JarvisAuth.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);
var connectionStringSqlite = builder.Configuration.GetConnectionString("Sqlite");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddSqliteDbContext(connectionStringSqlite);
builder.Services.AddSqliteHealthCheck(connectionStringSqlite);
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
builder.Services.RepositoriesDependencies();
builder.Services.ServicesDependencies();
builder.Services.ConfigureSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/api/healthz", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

app.Run();

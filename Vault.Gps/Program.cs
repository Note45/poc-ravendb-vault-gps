using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using vault_gps.Extensions.ApplicatonService;
using vault_gps.Extensions.Database;
using vault_gps.Extensions.Validations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddFluentValidationRulesToSwagger();

var configure = builder.Configuration;

builder.Services
    .AddEndpointsApiExplorer()
    .AddDatabaseConfigs(configure)
    .AddDatabase()
    .AddRepositories()
    .AddServices()
    .AddValidations()
    .AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using vault_gps.Application.Behaviors;
using vault_gps.Extensions.ApplicatonService;
using vault_gps.Extensions.Database;
using vault_gps.Extensions.Validations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddFluentValidationRulesToSwagger();

var configure = builder.Configuration;

// Register MediatR with handlers and behaviors
builder.Services.AddMediatR(typeof(Program).Assembly);

// Register ValidationBehavior manually for MediatR 11.x
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services
    .AddEndpointsApiExplorer()
    .AddDatabaseConfigs(configure)
    .AddDatabase()
    .AddDatabaseIndex()
    .AddRepositories()
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

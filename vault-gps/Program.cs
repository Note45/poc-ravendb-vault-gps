using vault_gps.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var configure = builder.Configuration;

builder.Services
    .AddDatabaseConfigs(configure)
    .AddDatabase()
    .AddRepositories()
    .AddServices();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

using vault_gps.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configure = builder.Configuration;

builder.Services
    .AddEndpointsApiExplorer()
    .AddDatabaseConfigs(configure)
    .AddDatabase()
    .AddRepositories()
    .AddServices()
    .AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

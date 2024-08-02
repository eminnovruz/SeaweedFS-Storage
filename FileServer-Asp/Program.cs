
using FileServer_Asp.DependencyInjections;
using FileServer_Asp.Middlewares;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProjectServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var ipListJson = app.Configuration["ipwhitelist"];
var allowedIps = JsonSerializer.Deserialize<string[]>(ipListJson);

app.Use(async (context, next) =>
{
    var middleware = new IpWhitelistMiddleWare(next, allowedIps);
    await middleware.InvokeAsync(context);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

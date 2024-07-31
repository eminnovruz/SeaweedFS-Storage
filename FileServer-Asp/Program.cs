using FileServer_Asp.Configurations;
using FileServer_Asp.Services;
using FileServer_Asp.Services.Abstract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<SeaweedConfiguration>(builder.Configuration.GetSection("SeaweedFS"));
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddHttpClient<IFileService, FileService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

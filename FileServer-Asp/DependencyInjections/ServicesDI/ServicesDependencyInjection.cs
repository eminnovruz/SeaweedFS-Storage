using FileServer_Asp.Configurations.MongoDb;
using FileServer_Asp.Configurations.SeaweedFs;
using FileServer_Asp.Data;
using FileServer_Asp.Services;
using FileServer_Asp.Services.Abstract;
using Serilog;
using System.Configuration;

namespace FileServer_Asp.DependencyInjections;

public static class ServicesDependencyInjection
{
    public static void AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SeaweedConfiguration>(configuration.GetSection("SeaweedFS"));

        services.Configure<MongoDbConfiguration>(configuration.GetSection("MongoDb"));

        services.AddScoped<IFileRegisterService, FileRegisterService>();

        services.AddSingleton<MongoDbContext>();

        services.AddScoped<IFileService, FileService>();

        services.AddHttpClient<IFileService, FileService>();

        ConfigureSerilog();
    }

    private static void ConfigureSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}

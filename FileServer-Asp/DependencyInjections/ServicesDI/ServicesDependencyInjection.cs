using FileServer_Asp.Configurations.MongoDb;
using FileServer_Asp.Configurations.SeaweedFs;
using FileServer_Asp.Data;
using FileServer_Asp.Services;
using FileServer_Asp.Services.Abstract;
using Serilog;
using System.Configuration;

namespace FileServer_Asp.DependencyInjections;

/// <summary>
/// Provides extension methods for configuring services and dependencies in the application.
/// </summary>
public static class ServicesDependencyInjection
{
    /// <summary>
    /// Adds project-specific services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The application configuration.</param>
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

    /// <summary>
    /// Configures Serilog logging with specified settings.
    /// </summary>
    private static void ConfigureSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}

using FileServer_Asp.Configurations;
using FileServer_Asp.Services;
using FileServer_Asp.Services.Abstract;
using Serilog;

namespace FileServer_Asp.DependencyInjection;

public static class ProjectDependencyInjection
{
    public static void AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SeaweedConfiguration>(configuration.GetSection("SeaweedFS"));

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

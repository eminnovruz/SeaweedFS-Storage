using FileServer_Asp.Configurations.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;

namespace FileServer_Asp.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbConfiguration> settings)
    {
        MongoClient _client = new MongoClient(settings.Value.ConnectionString);

        _database = _client.GetDatabase(settings.Value.DatabaseName);

        Log.Information("Application connected to database successfully.");

        Log.Information("Connected: " + settings.Value.DatabaseName);
    }

}

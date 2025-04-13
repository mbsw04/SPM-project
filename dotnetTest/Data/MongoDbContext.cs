namespace dotnetTest.Data;

using MongoDB.Driver;
using Microsoft.Extensions.Logging;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly ILogger<MongoDbContext> _logger;

    public MongoDbContext(IConfiguration configuration, ILogger<MongoDbContext> logger)
    {
        _logger = logger;
        try
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING") 
                ?? configuration.GetSection("MongoDB:ConnectionString").Value;
                
            var databaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME") 
                ?? configuration.GetSection("MongoDB:DatabaseName").Value;

            if (string.IsNullOrEmpty(connectionString))
            {
                _logger.LogError("MongoDB connection string is not configured");
                throw new InvalidOperationException("MongoDB connection string is not configured. Please set the MONGODB_CONNECTION_STRING environment variable.");
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                _logger.LogError("MongoDB database name is not configured");
                throw new InvalidOperationException("MongoDB database name is not configured. Please set the MONGODB_DATABASE_NAME environment variable.");
            }

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            
            // Test the connection
            _database.RunCommandAsync((Command<MongoDB.Bson.BsonDocument>)"{ping:1}").Wait();
            _logger.LogInformation("Successfully connected to MongoDB");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize MongoDB connection");
            throw;
        }
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}

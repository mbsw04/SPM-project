namespace dotnetTest.Data;

using MongoDB.Driver;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING") 
            ?? configuration.GetSection("MongoDB:ConnectionString").Value;
            
        var databaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME") 
            ?? configuration.GetSection("MongoDB:DatabaseName").Value;

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("MongoDB connection string is not configured. Please set the MONGODB_CONNECTION_STRING environment variable.");
        }

        if (string.IsNullOrEmpty(databaseName))
        {
            throw new InvalidOperationException("MongoDB database name is not configured. Please set the MONGODB_DATABASE_NAME environment variable.");
        }

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}

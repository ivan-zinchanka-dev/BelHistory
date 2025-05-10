using BelHistory.Domain.Database.Objects;
using BelHistory.Domain.Settings;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace BelHistory.Domain.Database.Services;

internal class DatabaseService
{
    private readonly ConnectionSettings _connectionSettings;
    private readonly IMongoDatabase _database;
    private readonly MongoClient _client;
    private readonly GridFSBucket _fileBucket;

    public IMongoCollection<HistoricalDocument> HistoricalDocs { get; private set; }
    public IMongoCollection<LocalizedString> Categories { get; private set; }
    public IMongoCollection<LocalizedString> SubCategories { get; private set; }
    public IMongoCollection<LocalizedString> Languages { get; private set; }
    
    public DatabaseService(ConnectionSettings connectionSettings)
    {
        _connectionSettings = connectionSettings;

        _client = new MongoClient(_connectionSettings.ConnectionString);
        _database = _client.GetDatabase(_connectionSettings.DatabaseName);
        _fileBucket = new GridFSBucket(_database);

        HistoricalDocs = _database.GetCollection<HistoricalDocument>("historicalDocs");
        Categories = _database.GetCollection<LocalizedString>("categories");
        SubCategories = _database.GetCollection<LocalizedString>("subCategories");
        Languages = _database.GetCollection<LocalizedString>("languages");
        
    }

    public void Test()
    {
        Categories.Find(FilterDefinition<LocalizedString>.Empty).FirstOrDefault();
        
    }
    
    
}
using BelHistory.Domain.Database.Objects;
using BelHistory.Domain.Settings;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Category = BelHistory.Domain.Database.Objects.Category;
using HistoricalDocument = BelHistory.Domain.Database.Objects.HistoricalDocument;

namespace BelHistory.Domain.Database.Services;

internal class DatabaseService
{
    private readonly ConnectionSettings _connectionSettings;
    private readonly IMongoDatabase _database;
    private readonly MongoClient _client;

    public IMongoCollection<HistoricalDocument> HistoricalDocs { get; private set; }
    public IMongoCollection<Category> Categories { get; private set; }
    public IMongoCollection<Language> Languages { get; private set; }
    public FileExtractor FileExtractor { get; private set; }
    public FileUploader FileUploader { get; private set; }

    public DatabaseService(ConnectionSettings connectionSettings)
    {
        _connectionSettings = connectionSettings;

        _client = new MongoClient(_connectionSettings.ConnectionString);
        _database = _client.GetDatabase(_connectionSettings.DatabaseName);

        var fileBucket = new GridFSBucket(_database);
        FileExtractor = new FileExtractor(fileBucket);
        FileUploader = new FileUploader(fileBucket);

        HistoricalDocs = _database.GetCollection<HistoricalDocument>("historicalDocs");
        Categories = _database.GetCollection<Category>("categories");
        Languages = _database.GetCollection<Language>("languages");
    }
}
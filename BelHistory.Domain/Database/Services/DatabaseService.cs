using BelHistory.Domain.API.Models;
using BelHistory.Domain.Database.Objects;
using BelHistory.Domain.Database.Objects.Base;
using BelHistory.Domain.Settings;
using MongoDB.Bson;
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
    private readonly GridFSBucket _fileBucket;

    public IMongoCollection<HistoricalDocument> HistoricalDocs { get; private set; }
    public IMongoCollection<Category> Categories { get; private set; }
    public IMongoCollection<Language> Languages { get; private set; }
    
    public DatabaseService(ConnectionSettings connectionSettings)
    {
        _connectionSettings = connectionSettings;

        _client = new MongoClient(_connectionSettings.ConnectionString);
        _database = _client.GetDatabase(_connectionSettings.DatabaseName);
        _fileBucket = new GridFSBucket(_database);

        HistoricalDocs = _database.GetCollection<HistoricalDocument>("historicalDocs");
        Categories = _database.GetCollection<Category>("categories");
        Languages = _database.GetCollection<Language>("languages");
    }

    public async Task<FileExtractionResult?> ExtractFileAsync(ObjectId fileId)
    {
        if (fileId == ObjectId.Empty)
        {
            return null;
        }

        try
        {
            IAsyncCursor<GridFSFileInfo> cursor = 
                await _fileBucket.FindAsync(Builders<GridFSFileInfo>.Filter.Eq("_id", fileId));

            GridFSFileInfo fileInfo = await cursor.FirstOrDefaultAsync();

            if (fileInfo == null)
            {
                return null;
            }

            GridFSDownloadStream stream = await _fileBucket.OpenDownloadStreamAsync(fileId);

            return new FileExtractionResult(fileInfo.Filename, "application/octet-stream", stream);
        }
        catch
        {
            return null;
        }
    }
}
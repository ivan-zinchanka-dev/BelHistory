using BelHistory.Domain.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace BelHistory.Domain.Database.Services;

internal class FileExtractor
{
    private readonly GridFSBucket _fileBucket;

    public FileExtractor(GridFSBucket fileBucket)
    {
        _fileBucket = fileBucket;
    }

    public async Task<FileExtractionResult?> ExtractFileAsync(ObjectId fileId)
    {
        if (fileId == ObjectId.Empty)
        {
            return null;
        }

        IAsyncCursor<GridFSFileInfo> cursor = 
            await _fileBucket.FindAsync(Builders<GridFSFileInfo>.Filter.Eq("_id", fileId));

        GridFSFileInfo fileInfo = await cursor.FirstOrDefaultAsync();

        if (fileInfo == null)
        {
            return null;
        }

        GridFSDownloadStream stream = await _fileBucket.OpenDownloadStreamAsync(fileId);
            
        return new FileExtractionResult(GetFileName(fileInfo), GetFileContentType(fileInfo), stream);
    }

    private static string GetFileName(GridFSFileInfo fileInfo)
    {
        const char separator = '\\';
        
        return fileInfo.Filename.Split(separator).Last();
    }

    private static string GetFileContentType(GridFSFileInfo fileInfo)
    {
        const string key = "contentType";
        const string defaultType = "application/octet-stream";

        if (fileInfo.Metadata != null && fileInfo.Metadata.TryGetValue(key, out BsonValue bsonValue))
        {
            return bsonValue.AsString;
        }
        else
        {
            return defaultType;
        }
    }
}
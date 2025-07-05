using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace BelHistory.Domain.Database.Services;

internal class FileUploader
{
    private const string DefaultContentType = "application/octet-stream";
    
    private static readonly IReadOnlyDictionary<string, string> ContentTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "pdf", "application/pdf" },
        { "docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { "djvu", "image/vnd.djvu" },
        { "jpeg", "image/jpeg" },
        { "jpg", "image/jpeg" },
        { "png", "image/png" },
    };
    
    private readonly GridFSBucket _fileBucket;
    
    public FileUploader(GridFSBucket fileBucket)
    {
        _fileBucket = fileBucket;
    }
    
    public async Task UploadFileAsync(string filePath)
    {
        await using (FileStream stream = File.OpenRead(filePath))
        {
            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument
                {
                    { "contentType", GetContentType(Path.GetExtension(filePath)) }
                }
            };

            await _fileBucket.UploadFromStreamAsync(Path.GetFileName(filePath), stream, options);
        }
    }

    private static string GetContentType(string extension)
    {
        return ContentTypes.GetValueOrDefault(extension, DefaultContentType);
    }
}
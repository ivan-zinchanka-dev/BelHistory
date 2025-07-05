namespace BelHistory.Domain.API.Models;

public readonly struct FileExtractionResult
{
    public string FileName { get; }
    public string ContentType { get; }
    public Stream Stream { get; }
    
    public FileExtractionResult(string fileName, string contentType, Stream stream)
    {
        FileName = fileName;
        ContentType = contentType;
        Stream = stream;
    }
}
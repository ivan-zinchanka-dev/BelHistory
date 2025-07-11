namespace BelHistory.Domain.API.Models;

public class FileInstance
{
    public string Title { get; internal set; }
    public LocalizedObject Language { get; internal set; }
    public string FileId { get; internal set; }
}
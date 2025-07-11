namespace BelHistory.Domain.API.Models;

public class HistoricalDocument
{
    public string Id { get; internal set; }
    public LocalizedString Title { get; internal set; }
    public LocalizedString CreationTime { get; internal set; }
    public LocalizedString Author { get; internal set; }
    public LocalizedString Description { get; internal set; }
    public LocalizedObject Language { get; internal set; }
    public LocalizedObject Category { get; internal set; }
    public LocalizedObject SubCategory { get; internal set; }
    public List<FileInstance> FileInstances { get; internal set; }
    public string ImageId { get; internal set; }
    public List<string> Tags { get; internal set; }
}
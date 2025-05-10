namespace BelHistory.Domain.API.Models;

public struct HistoricalDocumentPath
{
    public LocalizedObject Category { get; private set; }
    public LocalizedObject SubCategory { get; private set; }
}
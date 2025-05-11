namespace BelHistory.Domain.API.Models;

public class HistoricalDocumentPath
{
    public string CategoryId { get; set; }
    public string SubCategoryId { get; set; }

    public HistoricalDocumentPath() { }
    
    public HistoricalDocumentPath(string categoryId)
    {
        CategoryId = categoryId;
    }
    
    public HistoricalDocumentPath(string categoryId, string subCategoryId)
    {
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
    }
}
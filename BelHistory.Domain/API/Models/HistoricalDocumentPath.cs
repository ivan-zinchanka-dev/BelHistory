namespace BelHistory.Domain.API.Models;

public class HistoricalDocumentPath
{
    public const string AnyCategory = "Any";
    
    public string CategoryId { get; set; }
    public string SubCategoryId { get; set; } = AnyCategory;

    public HistoricalDocumentPath() { }
    
    public HistoricalDocumentPath(string categoryId, string subCategoryId = AnyCategory)
    {
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
    }
}
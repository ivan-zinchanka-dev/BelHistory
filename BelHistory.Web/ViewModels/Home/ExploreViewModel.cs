using System.Globalization;
using BelHistory.Domain.API.Models;

namespace BelHistory.Web.ViewModels.Home;

public class ExploreViewModel
{
    public Category Category { get; private set; }
    public Category SubCategory { get; private set; }
    public IReadOnlyList<HistoricalDocument> Documents { get; private set; }

    public ExploreViewModel(Category category, IReadOnlyList<HistoricalDocument> documents)
    {
        Category = category;
        Documents = documents;
    }
    
    public ExploreViewModel(Category category,Category subCategory, IReadOnlyList<HistoricalDocument> documents)
    {
        Category = category;
        SubCategory = subCategory;
        Documents = documents;
    }
    
    public string GetPath(CultureInfo locale)
    {
        return SubCategory != null ? 
            $"{Category.LocalizedObject.Name.ToString(locale)} / {SubCategory?.LocalizedObject.Name.ToString(locale)}" : 
            $"{Category.LocalizedObject.Name.ToString(locale)}";
    }
    
}
using System.Globalization;
using BelHistory.Domain.API.Models;

namespace BelHistory.Domain.API.Extensions;

public static class HistoricalDocumentExtensions
{
    public static string GetFullPath(this HistoricalDocument historicalDocument, CultureInfo cultureInfo)
    {
        return $"{historicalDocument.Category.Name.ToString(cultureInfo)}/" +
               $"{historicalDocument.SubCategory.Name.ToString(cultureInfo)}/" +
               $"{historicalDocument.Title.ToString(cultureInfo)}";
    }
    
    public static string GetFullPath(this HistoricalDocument historicalDocument)
    {
        return GetFullPath(historicalDocument, CultureInfo.CurrentCulture);
    }
}
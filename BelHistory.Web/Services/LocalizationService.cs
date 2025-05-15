using System.Globalization;

namespace BelHistory.Web.Services;

public class LocalizationService : ILocalizationService
{
    public CultureInfo CurrentLocale { get; set; }
    
    public string Localize(string term)
    {
        throw new NotImplementedException();
    }

    public bool TryLocalize(string term, out string result)
    {
        throw new NotImplementedException();
    }
}
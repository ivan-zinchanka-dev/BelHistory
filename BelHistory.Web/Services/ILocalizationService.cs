using System.Globalization;

namespace BelHistory.Web.Services;

public interface ILocalizationService
{
    public CultureInfo CurrentLocale { get; set; }

    public string Localize(string term);
    public bool TryLocalize(string term, out string result);
}
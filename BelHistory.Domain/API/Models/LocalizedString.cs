using System.Globalization;

namespace BelHistory.Domain.API.Models;

public class LocalizedString
{
    public string Be { get; private set; }
    public string Ru { get; private set; }

    public LocalizedString(string be, string ru)
    {
        Be = be;
        Ru = ru;
    }

    public string ToString(CultureInfo locale)
    {
        if (locale != null && locale.Name.StartsWith(Constants.RussianLocaleName))
        {
            return Ru;
        }

        return Be;
    }

    public override string ToString() => ToString(CultureInfo.CurrentCulture);
}
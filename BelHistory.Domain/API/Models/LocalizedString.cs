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
}
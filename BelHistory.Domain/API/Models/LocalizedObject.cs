namespace BelHistory.Domain.API.Models;

public class LocalizedObject
{
    public string Id { get; private set; }
    public LocalizedString Name { get; private set; }

    public LocalizedObject(string id, LocalizedString name)
    {
        Id = id;
        Name = name;
    }
}
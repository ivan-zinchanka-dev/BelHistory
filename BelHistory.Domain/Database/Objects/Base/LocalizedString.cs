using MongoDB.Bson.Serialization.Attributes;

namespace BelHistory.Domain.Database.Objects.Base;

internal class LocalizedString
{
    [BsonElement("be")]
    public string Be { get; private set; }
    
    [BsonElement("ru")]
    public string Ru { get; private set; }
}
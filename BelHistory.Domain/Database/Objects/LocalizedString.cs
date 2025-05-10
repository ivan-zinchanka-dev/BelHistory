using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BelHistory.Domain.Database.Objects;

internal class LocalizedString
{
    [BsonElement("be")]
    public string Be { get; private set; }
    
    [BsonElement("ru")]
    public string Ru { get; private set; }
}
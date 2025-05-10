using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BelHistory.Domain.Database.Objects;

internal class LocalizedObject
{
    [BsonId] 
    public ObjectId Id { get; private set; }
    
    [BsonElement("name")]
    public LocalizedString Name { get; private set; }
}
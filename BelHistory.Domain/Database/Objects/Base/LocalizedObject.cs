using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BelHistory.Domain.Database.Objects.Base;

internal class LocalizedObject
{
    [BsonId] 
    public ObjectId Id { get; protected set; }
    
    [BsonElement("name")]
    public LocalizedString Name { get; protected set; }
}
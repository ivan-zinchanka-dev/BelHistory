using BelHistory.Domain.Database.Objects.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BelHistory.Domain.Database.Objects;

internal class Category : LocalizedObject
{
    [BsonElement("parentId")]
    public ObjectId ParentId { get; private set; }
}
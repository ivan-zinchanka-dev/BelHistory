using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace BelHistory.Domain.Database.Objects;

public class HistoricalDocument
{
    [BsonId]
    public ObjectId Id { get; private set; }
    
    [BsonElement("title"), Required]
    public LocalizedString Title { get; private set; }
    
    [BsonElement("creationTime"), Required]
    public LocalizedString CreationTime { get; private set; }
    
    [BsonElement("author")]
    public LocalizedString Author { get; private set; }

    [BsonElement("language"), Required]
    public MongoDBRef Language { get; private set; }
    
    [BsonElement("category"), Required]
    public MongoDBRef Category { get; private set; }
    
    [BsonElement("subCategory"), Required]
    public MongoDBRef SubCategory { get; private set; }
    
    [BsonElement("fileId")]
    public ObjectId FileId { get; private set; }

}
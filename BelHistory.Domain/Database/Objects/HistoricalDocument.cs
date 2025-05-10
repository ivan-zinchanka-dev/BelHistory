using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace BelHistory.Domain.Database.Objects;

internal class HistoricalDocument
{
    [BsonId]
    public ObjectId Id { get; private set; }
    
    [BsonElement("title"), Required]
    public LocalizedString Title { get; private set; }
    
    [BsonElement("creationTime"), Required]
    public LocalizedString CreationTime { get; private set; }
    
    [BsonElement("author")]
    public LocalizedString Author { get; private set; }

    [BsonElement("languageId")]
    public ObjectId LanguageId { get; private set; }
    
    [BsonElement("categoryId")]
    public ObjectId CategoryId { get; private set; }
    
    [BsonElement("subCategoryId")]
    public ObjectId SubCategoryId { get; private set; }
    
    [BsonElement("fileId")]
    public ObjectId FileId { get; private set; }

    [BsonElement("tags"), Required]
    public List<string> Tags { get; private set; }

}
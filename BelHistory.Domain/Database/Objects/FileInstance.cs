using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BelHistory.Domain.Database.Objects;

internal class FileInstance
{
    [BsonElement("title"), Required]
    public string Title { get; private set; }
    
    [BsonElement("languageId")]
    public ObjectId LanguageId { get; private set; }
    
    [BsonElement("fileId")]
    public ObjectId FileId { get; private set; }
}
using BelHistory.Domain.API.Models;
using BelHistory.Domain.Database.Services;
using BelHistory.Domain.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;

using HistoricalDocumentDbo = BelHistory.Domain.Database.Objects.HistoricalDocument;

namespace BelHistory.Domain.API.Services;

public class HistoricalArchive
{
    private readonly DatabaseService _databaseService;
    private readonly Dictionary<ObjectId, LocalizedObject> _sharedObjects = new ();
    
    internal HistoricalArchive(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        InitializeSharedObjects();
    }
    
    private void InitializeSharedObjects()
    {
        var dbObjects = _databaseService.Categories.All().ToList();
        dbObjects.AddRange(_databaseService.SubCategories.All().ToList());
        dbObjects.AddRange(_databaseService.Languages.All().ToList());

        foreach (var dbObject in dbObjects)
        {
            _sharedObjects.Add(dbObject.Id, dbObject.ToApiModel());
        }
    }
    
    public async Task<IReadOnlyList<HistoricalDocument>> GetPagedDocumentsByPath(
        HistoricalDocumentPath path,
        int pageIndex, 
        int pageSize)
    {
        if (pageIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageIndex), "Page index must be greater than or equal to zero.");
        }

        List<HistoricalDocumentDbo> foundObjects = await _databaseService.HistoricalDocs.
            Find(doc=>
                doc.CategoryId == ObjectId.Parse(path.Category.Id) && 
                doc.SubCategoryId == ObjectId.Parse(path.SubCategory.Id))
            .Skip(pageIndex * pageSize)
            .Limit(pageSize)
            .ToListAsync();
        
        return foundObjects
            .Select(MapToApiModel)
            .ToList();
    }
    
    private HistoricalDocument MapToApiModel(HistoricalDocumentDbo document)
    {
        _sharedObjects.TryGetValue(document.LanguageId, out LocalizedObject language);
        _sharedObjects.TryGetValue(document.CategoryId, out LocalizedObject category);
        _sharedObjects.TryGetValue(document.SubCategoryId, out LocalizedObject subCategory);
        
        return new HistoricalDocument()
        {
            Id = document.Id.ToString(),
            Title = document.Title.ToApiModel(),
            CreationTime = document.CreationTime.ToApiModel(),
            Author = document.Author.ToApiModel(),
            Language = language,
            Category = category,
            SubCategory = subCategory,
            FileId = document.FileId.ToString(),
            Tags = document.Tags
        };
    }
}
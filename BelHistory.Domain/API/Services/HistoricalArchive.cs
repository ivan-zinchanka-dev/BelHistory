using BelHistory.Domain.API.Models;
using BelHistory.Domain.Database.Services;
using BelHistory.Domain.Extensions;
using BelHistory.Domain.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

using Category = BelHistory.Domain.API.Models.Category;
using CategoryDbo = BelHistory.Domain.Database.Objects.Category;
using HistoricalDocument = BelHistory.Domain.API.Models.HistoricalDocument;
using HistoricalDocumentDbo = BelHistory.Domain.Database.Objects.HistoricalDocument;

namespace BelHistory.Domain.API.Services;

public class HistoricalArchive
{
    private readonly DatabaseService _databaseService;

    private List<Category> _catalog;
    private readonly SharedLocalizedObjects _sharedObjects = new ();
    
    public HistoricalArchive()
    {
        //TODO Add configuration
        _databaseService = new DatabaseService(new ConnectionSettings()
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "BelHistory",
        });
        
        InitializeSharedObjects();
    }
    
    private void InitializeSharedObjects()
    {
        foreach (var category in _databaseService.Categories.All().ToList())
        {
            _sharedObjects.Categories.Add(category.Id, category.ToApiModel());
        }
        
        foreach (var language in _databaseService.Languages.All().ToList())
        {
            _sharedObjects.Languages.Add(language.Id, language.ToApiModel());
        }
    }

    public async Task<IReadOnlyList<Category>> GetCatalogAsync() 
    {
        if (_catalog == null)
        {
            _catalog = new List<Category>();
            
            FilterDefinition<CategoryDbo> topCategoryFilter = Builders<CategoryDbo>.Filter
                .Exists(category => category.ParentId, false);
            
            List<CategoryDbo> topCategoryObjects = await _databaseService.Categories
                .Find(topCategoryFilter)
                .ToListAsync();
            
            foreach (CategoryDbo categoryObject in topCategoryObjects)
            {
                List<CategoryDbo> subCategoryObjects = await _databaseService.Categories
                    .Find(category => category.ParentId == categoryObject.Id)
                    .ToListAsync();

                List<Category> subCategories = subCategoryObjects
                    .Select(obj => new Category(obj.ToApiModel()))
                    .ToList();
                
                _catalog.Add(new Category(categoryObject.ToApiModel()).SetSubCategories(subCategories));
            }
        }
        
        return _catalog;
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

        List<HistoricalDocumentDbo> foundObjects = await _databaseService.HistoricalDocs
            .Find(doc=>
                doc.CategoryId == MapId(path.CategoryId) && 
                (path.SubCategoryId == HistoricalDocumentPath.AnyCategory || 
                 doc.SubCategoryId == MapId(path.SubCategoryId)))
            .Skip(pageIndex * pageSize)
            .Limit(pageSize)
            .ToListAsync();
        
        return foundObjects
            .Select(MapToApiModel)
            .ToList();
    }

    public async Task<HistoricalDocument> GetDocumentById(string documentId)
    {
        ObjectId id = MapId(documentId);
        
        HistoricalDocumentDbo foundDocument = await _databaseService.HistoricalDocs
            .Find(doc=> doc.Id == id)
            .FirstOrDefaultAsync();
        
        return MapToApiModel(foundDocument);
    }

    private ObjectId MapId(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return ObjectId.Empty;
        }
        else if (ObjectId.TryParse(id, out ObjectId parsedId))
        {
            return parsedId;
        }
        else return ObjectId.Empty;
    }

    private HistoricalDocument MapToApiModel(HistoricalDocumentDbo document)
    {
        if (document == null)
        {
            return null;
        }

        _sharedObjects.Languages.TryGetValue(document.LanguageId, out LocalizedObject language);
        _sharedObjects.Categories.TryGetValue(document.CategoryId, out LocalizedObject category);
        _sharedObjects.Categories.TryGetValue(document.SubCategoryId, out LocalizedObject subCategory);
        
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
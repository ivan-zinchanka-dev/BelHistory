using BelHistory.Domain.API.Models;
using MongoDB.Bson;

namespace BelHistory.Domain.API.Services;

internal readonly struct SharedLocalizedObjects
{
    public readonly Dictionary<ObjectId, LocalizedObject> Categories;
    public readonly Dictionary<ObjectId, LocalizedObject> Languages;

    public SharedLocalizedObjects(
        Dictionary<ObjectId, LocalizedObject> categories, 
        Dictionary<ObjectId, LocalizedObject> languages)
    {
        Categories = categories;
        Languages = languages;
    }
}
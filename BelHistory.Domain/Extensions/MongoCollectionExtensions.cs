using MongoDB.Driver;

namespace BelHistory.Domain.Extensions;

internal static class MongoCollectionExtensions
{
    public static IFindFluent<TDocument, TDocument> All<TDocument>(this IMongoCollection<TDocument> collection)
    {
        return collection.Find(FilterDefinition<TDocument>.Empty);
    }
}
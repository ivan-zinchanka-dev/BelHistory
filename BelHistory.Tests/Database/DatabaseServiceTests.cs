using BelHistory.Domain.Database.Objects;
using BelHistory.Domain.Extensions;
using MongoDB.Driver;
using Xunit.Abstractions;

namespace BelHistory.Tests.Database;

public class DatabaseServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _databaseFixture;
    private readonly ITestOutputHelper _outputHelper;

    public DatabaseServiceTests(DatabaseFixture databaseFixture, ITestOutputHelper outputHelper)
    {
        _databaseFixture = databaseFixture;
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task CheckCategories()
    {
        List<Category> categories = await _databaseFixture.Service.Categories.All().ToListAsync();

        foreach (Category category in categories)
        {
            string categoryOutput = 
                $"\nId: {category.Id}\nName(Be): {category.Name.Be}\nName(Ru): {category.Name.Ru}\nParentId: {category.ParentId}";
            _outputHelper.WriteLine(categoryOutput);
        }
        
        Assert.True(categories.Count > 0);
    }
    
    [Fact]
    public async Task CheckDocument()
    {
        List<HistoricalDocument> historicalDocs = await _databaseFixture.Service.HistoricalDocs
            .Find(doc => doc.Title.Be == "Судзебнік Казіміра 1468 г.").ToListAsync();
        
        Assert.True(historicalDocs.Count == 1);
    }
}
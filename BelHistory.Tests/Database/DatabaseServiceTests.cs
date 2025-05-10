using BelHistory.Domain.Database.Objects;
using BelHistory.Domain.Database.Services;
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
    public async Task Show()
    {
        var result = await _databaseFixture.Service.Categories.Find(x=> x.Ru == "Книги").Limit(1).ToListAsync();
        Assert.NotNull(result);
    }

    [Fact]
    public void Test1()
    {
        _outputHelper.WriteLine("XUnit test");
    }
}
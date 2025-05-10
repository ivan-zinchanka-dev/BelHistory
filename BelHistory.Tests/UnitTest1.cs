using Xunit;
using Xunit.Abstractions;

namespace BelHistory.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        _testOutputHelper.WriteLine("XUnit test");
    }
}
using BelHistory.Domain.Database.Services;
using BelHistory.Domain.Settings;

namespace BelHistory.Tests.Database;

public class DatabaseFixture : IDisposable
{
    internal DatabaseService Service { get; }

    public DatabaseFixture()
    {
        Service = new DatabaseService(new ConnectionSettings()
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "BelHistory",
        });
    }
    
    public void Dispose()
    {
        
    }
}
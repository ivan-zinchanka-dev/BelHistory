using BelHistory.Domain.Database.Services;

namespace BelHistory.Domain.API.Services;

public class HistoricalArchive
{
    private DatabaseService _databaseService;
    
    internal HistoricalArchive(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    
    
}
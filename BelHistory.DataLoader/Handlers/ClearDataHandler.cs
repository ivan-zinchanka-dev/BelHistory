using System.Diagnostics;
using BelHistory.DataLoader.Services;
using Microsoft.Extensions.Logging;

namespace BelHistory.DataLoader.Handlers;

public class ClearDataHandler : BaseShellActionHandler
{
    private const string DropDatabaseCommand = "--eval \"db.dropDatabase()\"";
    
    public ClearDataHandler(ILogger<ClearDataHandler> logger, EnvironmentExecutableFinder executableFinder)
        : base(logger, executableFinder) { }

    public async Task ExecuteAsync()
    {
        if (!FindMongoShellExecutable())
        {
            return;
        }
        
        Process process = CreateMongoShellProcess(DropDatabaseCommand);
        process.Start();
        await process.WaitForExitAsync();

        Logger.LogInformation($"Удаление базы данных завершено с кодом {process.ExitCode}");
    }
}
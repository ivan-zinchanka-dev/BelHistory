using System.Diagnostics;
using BelHistory.DataLoader.Services;
using Microsoft.Extensions.Logging;

namespace BelHistory.DataLoader.Handlers;

public abstract class BaseShellActionHandler
{
    private const string MongoShellPath = "mongosh.exe";
    private const string DatabaseName = "BelHistory";

    protected readonly ILogger Logger;
    private readonly EnvironmentExecutableFinder _executableFinder;

    private string _mongoShellFileName;
    
    protected BaseShellActionHandler(ILogger logger, EnvironmentExecutableFinder executableFinder)
    {
        Logger = logger;
        _executableFinder = executableFinder;
    }

    protected bool FindMongoShellExecutable()
    {
        _mongoShellFileName = _executableFinder.FindExecutable(MongoShellPath);

        if (_mongoShellFileName != null)
        {
            return true;
        }
        else
        {
            Logger.LogError("Исполняемый файл MongoShell не обнаружен");
            return false;
        }
    }
    
    protected Process CreateMongoShellProcess(string mongoShellCommand)
    {
        if (_mongoShellFileName == null && !FindMongoShellExecutable())
        {
            return null;
        }

        return new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = _mongoShellFileName,
                Arguments = $"{DatabaseName} {mongoShellCommand}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
    }
    
}
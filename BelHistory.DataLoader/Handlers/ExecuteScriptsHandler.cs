using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace BelHistory.DataLoader.Handlers;

internal class ExecuteScriptsHandler
{
    private const string PathVariable = "PATH";
    private const string MongoShellPath = "mongosh.exe";
    private const string DatabaseName = "BelHistory";

    private static readonly string[] ScriptNames =
    {
        "enums_data.js",
        "history_docs_data.js",
    };

    private ILogger<ExecuteScriptsHandler> _logger;

    public ExecuteScriptsHandler(ILogger<ExecuteScriptsHandler> logger)
    {
        _logger = logger;
    }

    public async Task ExecuteAsync(string scriptsDirectoryPath)
    {
        if (!Directory.Exists(scriptsDirectoryPath))
        {
            _logger.LogError($"Дирректория \"{scriptsDirectoryPath}\" не найдена");
            return;
        }

        string mongoShellFile = FindExecutableInPath(MongoShellPath);

        if (mongoShellFile == null)
        {
            _logger.LogError("Исполняемый файл MongoShell не обнаружен");
            return;
        }

        foreach (string scriptName in ScriptNames)
        {
            _logger.LogInformation($"Запуск скрипта: {scriptName}");
            
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = mongoShellFile,
                    Arguments = $"{DatabaseName} \"{Path.Combine(scriptsDirectoryPath, scriptName)}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            await process.WaitForExitAsync();

            _logger.LogInformation($"Скрипт {scriptName} выполнен с кодом {process.ExitCode}");
        }
    }

    private static string[] GetPaths(EnvironmentVariableTarget target)
    {
        return (Environment.GetEnvironmentVariable(PathVariable, target) ?? string.Empty)
            .Split(Path.PathSeparator);
    }

    private string FindExecutableInPath(string exeName)
    {
        IEnumerable<string> paths = GetPaths(EnvironmentVariableTarget.Machine)
            .Concat(GetPaths(EnvironmentVariableTarget.User))
            .Distinct();

        foreach (string path in paths)
        {
            try
            {
                string fullPath = Path.Combine(path.Trim(), exeName);
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка");
            }
        }

        return null;
    }
}
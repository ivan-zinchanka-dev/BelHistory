using System.Diagnostics;
using BelHistory.DataLoader.Services;
using Microsoft.Extensions.Logging;

namespace BelHistory.DataLoader.Handlers;

internal class ExecuteScriptsHandler : BaseShellActionHandler
{
    private static readonly string[] ScriptNames =
    {
        "enums_data.js",
        "history_docs_data.js",
    };

    public ExecuteScriptsHandler(ILogger<ExecuteScriptsHandler> logger, EnvironmentExecutableFinder executableFinder)
        : base(logger, executableFinder) { }

    public async Task ExecuteAsync(string scriptsDirectoryPath)
    {
        if (!Directory.Exists(scriptsDirectoryPath))
        {
            Logger.LogError($"Дирректория \"{scriptsDirectoryPath}\" не найдена");
            return;
        }

        if (!FindMongoShellExecutable())
        {
            return;
        }

        foreach (string scriptName in ScriptNames)
        {
            Logger.LogInformation($"Запуск скрипта: {scriptName}");

            string scriptFullName = Path.Combine(scriptsDirectoryPath, scriptName);
            
            Process process = CreateMongoShellProcess($"\"{scriptFullName}\"");
            process.Start();
            await process.WaitForExitAsync();

            Logger.LogInformation($"Скрипт {scriptName} выполнен с кодом {process.ExitCode}");
        }
    }
}
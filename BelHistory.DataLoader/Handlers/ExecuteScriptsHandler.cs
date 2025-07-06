using System.Diagnostics;

namespace BelHistory.DataLoader.Handlers;

internal class ExecuteScriptsHandler
{
    private const string PathVariable = "PATH";
    private const string MongoShellPath = "mongosh.exe";
    private const string DatabaseName = "BelHistory";

    private static readonly string[] Scripts =
    {
        "enums_data.js",
        "history_docs_data.js",
    };
    
    public async Task ExecuteAsync()
    {
        string mongoShellFile = FindExecutableInPath(MongoShellPath);
        
        foreach (string script in Scripts)
        {
            Console.WriteLine($"Запуск скрипта: {script}");
            
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = mongoShellFile,
                    Arguments = $"{DatabaseName} \"{script}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            
            if (!string.IsNullOrWhiteSpace(output))
                Console.WriteLine("Вывод:\n" + output);

            if (!string.IsNullOrWhiteSpace(error))
                Console.WriteLine("Ошибка:\n" + error);

            Console.WriteLine($"Скрипт {script} завершён (код {process.ExitCode})");
        }
    }

    private static string[] GetPaths(EnvironmentVariableTarget target)
    {
        return (Environment.GetEnvironmentVariable(PathVariable, target) ?? string.Empty)
            .Split(Path.PathSeparator);
    }

    private static string FindExecutableInPath(string exeName)
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
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        return null;
    }
}
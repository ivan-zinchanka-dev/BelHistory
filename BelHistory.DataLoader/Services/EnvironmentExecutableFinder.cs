using Microsoft.Extensions.Logging;

namespace BelHistory.DataLoader.Services;

public class EnvironmentExecutableFinder
{
    private const string PathVariableName = "PATH";
    private readonly ILogger<EnvironmentExecutableFinder> _logger;

    public EnvironmentExecutableFinder(ILogger<EnvironmentExecutableFinder> logger)
    {
        _logger = logger;
    }

    public string FindExecutable(string exeName)
    {
        if (string.IsNullOrEmpty(exeName) || string.IsNullOrWhiteSpace(exeName))
        {
            _logger.LogError(new ArgumentException(
                "Имя файла должно содежать символы", nameof(exeName)), 
                "Некорректное имя исполняемого файла");

            return null;
        }

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
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Во время поиска исполняемого файла возникло исключение");
            }
        }

        return null;
    }
    
    private static string[] GetPaths(EnvironmentVariableTarget target)
    {
        return (Environment.GetEnvironmentVariable(PathVariableName, target) ?? string.Empty)
            .Split(Path.PathSeparator);
    }
}
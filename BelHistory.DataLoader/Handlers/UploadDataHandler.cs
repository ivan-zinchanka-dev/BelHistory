using Microsoft.Extensions.Logging;

namespace BelHistory.DataLoader.Handlers;

internal class UploadDataHandler
{
    private const string FilesFolderName = "Files";
    private const string ScriptsFolderName = "Scripts";

    private readonly ILogger<UploadDataHandler> _logger;
    private readonly UploadFilesHandler _uploadFilesHandler;
    private readonly ExecuteScriptsHandler _executeScriptsHandler;
    
    public UploadDataHandler(
        ILogger<UploadDataHandler> logger, 
        UploadFilesHandler uploadFilesHandler, 
        ExecuteScriptsHandler executeScriptsHandler)
    {
        _logger = logger;
        _uploadFilesHandler = uploadFilesHandler;
        _executeScriptsHandler = executeScriptsHandler;
    }

    public async Task UploadDataAsync(string rootDirectoryPath)
    {
        if (Directory.Exists(rootDirectoryPath))
        {
            await _uploadFilesHandler.UploadFilesAsync(Path.Combine(rootDirectoryPath, FilesFolderName));
            await _executeScriptsHandler.ExecuteAsync(Path.Combine(rootDirectoryPath, ScriptsFolderName));
        }
        else
        {
            _logger.LogError("Указанной дирректории не существует");
        }
    }
}
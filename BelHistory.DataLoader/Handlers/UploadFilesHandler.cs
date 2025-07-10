using BelHistory.Domain.API.Services;
using Microsoft.Extensions.Logging;

namespace BelHistory.DataLoader.Handlers;

internal class UploadFilesHandler
{
    private const string AllFilesSearchPattern = "*.*";
    
    private readonly ILogger<UploadFilesHandler> _logger;
    private readonly HistoricalArchive _archive;

    public UploadFilesHandler(ILogger<UploadFilesHandler> logger, HistoricalArchive archive)
    {
        _logger = logger;
        _archive = archive;
    }

    public async Task UploadFilesAsync(string filesDirectoryPath)
    {
        if (!Directory.Exists(filesDirectoryPath))
        {
            _logger.LogError($"Дирректория \"{filesDirectoryPath}\" не найдена");
            return;
        }
        
        string[] allFiles = Directory.GetFiles(filesDirectoryPath, AllFilesSearchPattern, SearchOption.AllDirectories);
        await _archive.UploadFilesAsync(allFiles);
        
        _logger.LogInformation($"Файлы загружены в БД");
    }
}
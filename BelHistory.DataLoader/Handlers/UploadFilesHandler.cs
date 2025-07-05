using BelHistory.Domain.API.Services;

namespace BelHistory.DataLoader.Handlers;

internal class UploadFilesHandler
{
    private readonly HistoricalArchive _archive;

    public UploadFilesHandler(HistoricalArchive archive)
    {
        _archive = archive;
    }

    public async Task UploadFilesAsync(string filesDirectoryPath)
    {
        string[] allFiles = Directory.GetFiles(filesDirectoryPath, "*.*", SearchOption.AllDirectories);
        await _archive.UploadFilesAsync(allFiles);
    }
}
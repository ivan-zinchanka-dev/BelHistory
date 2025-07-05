using System.CommandLine;
using System.CommandLine.Invocation;
using BelHistory.DataLoader.Handlers;
using BelHistory.Domain.API.Services;

namespace BelHistory.DataLoader;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Утилита для добавления данных");

        var loadFilesCommand = new Command("files", "Загружает файлы в БД");
        var directoryArg = new Argument<string>("dir");
        
        var uploadFilesHandler = new UploadFilesHandler(new HistoricalArchive());
        
        loadFilesCommand.Add(directoryArg);
        loadFilesCommand.SetHandler(uploadFilesHandler.UploadFilesAsync, directoryArg);
        
        rootCommand.Add(loadFilesCommand);
        
        return await rootCommand.InvokeAsync(args);
    }
}
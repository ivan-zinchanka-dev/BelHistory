using System.CommandLine;
using BelHistory.DataLoader.Handlers;
using BelHistory.DataLoader.Services;
using BelHistory.Domain.API.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BelHistory.DataLoader;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        IServiceProvider serviceProvider = ConfigureServices();
        
        var rootCommand = new RootCommand("Утилита для добавления данных");

        var uploadCommand = new Command("upload", "Загружает данные в БД");
        var rootDirArg = new Argument<string>("rootDir", "Корневая папка с данными");
        
        var uploadDataHandler = serviceProvider.GetRequiredService<UploadDataHandler>();
        uploadCommand.Add(rootDirArg);
        uploadCommand.SetHandler(uploadDataHandler.UploadDataAsync, rootDirArg);
        rootCommand.Add(uploadCommand);
        
        var clearCommand = new Command("clear", "Удаляет БД");
        
        var clearDataHandler = serviceProvider.GetRequiredService<ClearDataHandler>();
        clearCommand.SetHandler(clearDataHandler.ExecuteAsync);
        rootCommand.Add(clearCommand);
        
        return await rootCommand.InvokeAsync(args);
    }

    private static IServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton<HistoricalArchive>()
            .AddSingleton<EnvironmentExecutableFinder>()
            .AddSingleton<UploadFilesHandler>()
            .AddSingleton<ExecuteScriptsHandler>()
            .AddSingleton<UploadDataHandler>()
            .AddSingleton<ClearDataHandler>()
            .AddLogging(configure =>
            {
                configure.AddConsole();
                configure.SetMinimumLevel(LogLevel.Debug);
            })
            .BuildServiceProvider();
    }
}
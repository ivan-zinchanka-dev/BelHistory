using System.CommandLine;
using BelHistory.DataLoader.Handlers;
using BelHistory.Domain.API.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BelHistory.DataLoader;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Утилита для добавления данных");

        var installCommand = new Command("upload", "Загружает данные в БД");
        var rootDirArg = new Argument<string>("rootDir", "Корневая папка с данными");

        IServiceProvider serviceProvider = ConfigureServices();
        var uploadDataHandler = serviceProvider.GetRequiredService<UploadDataHandler>();
        
        installCommand.Add(rootDirArg);
        installCommand.SetHandler(uploadDataHandler.UploadDataAsync, rootDirArg);
        
        rootCommand.Add(installCommand);
        
        return await rootCommand.InvokeAsync(args);
    }

    private static IServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton<HistoricalArchive>()
            .AddSingleton<UploadFilesHandler>()
            .AddSingleton<ExecuteScriptsHandler>()
            .AddLogging(configure =>
            {
                configure.AddConsole();
                configure.SetMinimumLevel(LogLevel.Debug);
            })
            .AddSingleton<UploadDataHandler>()
            .BuildServiceProvider();
    }
}
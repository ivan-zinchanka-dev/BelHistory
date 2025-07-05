using System.CommandLine;
using System.CommandLine.Invocation;

namespace BelHistory.DataLoader;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        RootCommand rootCommand = new RootCommand("Утилита для добавления данных");

        var loadFilesCommand = new Command("files", "Загружает файлы в БД");
        var directoryArg = new Argument<string>("dir");
        
        
        loadFilesCommand.Add(directoryArg);
        loadFilesCommand.SetHandler( directoryPath =>
        {
            Console.WriteLine("Path: " + directoryPath);
            
        }, directoryArg);
        
        
        rootCommand.Add(loadFilesCommand);
        
        return await rootCommand.InvokeAsync(args);
    }
}
using BelHistory.Domain.API.Services;
using Microsoft.AspNetCore.StaticFiles;

namespace BelHistory.Web;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        builder.Services.AddSingleton<HistoricalArchive>();
        
        WebApplication app = builder.Build();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        
        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = ForceDisableCaching
        });
        
        app.Run();
    }
    
    private static void ForceDisableCaching(StaticFileResponseContext context)
    {
        context.Context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
        context.Context.Response.Headers["Pragma"] = "no-cache";
        context.Context.Response.Headers["Expires"] = "-1";
    }
}
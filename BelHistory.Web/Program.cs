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
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
        
        WebApplication app = builder.Build();
        
        /*app.MapGet("/", context =>
        {
            context.Response.Redirect("/ru");
            return Task.CompletedTask;
        });*/
        
        app.MapControllerRoute(
            name: "defaultWithoutLang",
            pattern: "{controller=Home}/{action=Index}/{id?}",
            defaults: new { lang = "ru" });

        app.MapControllerRoute(
            name: "defaultWithLang",
            pattern: "{lang}/{controller=Home}/{action=Index}/{id?}");
        
        
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
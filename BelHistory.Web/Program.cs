namespace BelHistory.Web;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllersWithViews();
        
        WebApplication app = builder.Build();

        //app.MapGet("/", () => "Hello World!");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        
        app.Run();
    }
}
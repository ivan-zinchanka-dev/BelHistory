using BelHistory.Domain.API.Models;
using BelHistory.Domain.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BelHistory.Web.Controllers;

public class HomeController : Controller
{
    private readonly HistoricalArchive _archive;
    private readonly ILogger<HomeController> _logger;
    
    public HomeController(HistoricalArchive archive, ILogger<HomeController> logger)
    {
        _archive = archive;
        _logger = logger;
    }
    
    public async Task<IActionResult> Index()
    {
        IReadOnlyList<Category> catalog = await _archive.GetCatalogAsync();
        return View(catalog);
    }

    public IActionResult Explore(string category, string subCategory)
    {

        return View();
    }
}
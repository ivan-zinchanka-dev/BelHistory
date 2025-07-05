using BelHistory.Domain.API.Models;
using BelHistory.Domain.API.Services;
using BelHistory.Web.ViewModels.Home;
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

    public async Task<IActionResult> Explore([FromQuery] HistoricalDocumentPath path)
    {
        IReadOnlyList<HistoricalDocument> historicalDocs = 
            await _archive.GetPagedDocumentsByPathAsync(path, 0, 10);
        
        IReadOnlyList<Category> catalog = await _archive.GetCatalogAsync();
        Category category = GetCategoryById(catalog, path.CategoryId);
        Category subCategory = GetCategoryById(catalog, path.SubCategoryId);
        
        return View(new ExploreViewModel(category, subCategory, historicalDocs));
    }

    public async Task<IActionResult> Details([FromQuery] string documentId)
    {
        HistoricalDocument historicalDoc = await _archive.GetDocumentByIdAsync(documentId);

        return View(historicalDoc);
    }

    public async Task<IActionResult> DownloadFile([FromQuery] string fileId)
    {
         FileExtractionResult? fileResult = await _archive.ExtractFileByIdAsync(fileId);

         if (fileResult.HasValue)
         {
             var file = fileResult.Value;
             return File(file.Stream, file.ContentType, file.FileName);
         }
         else
         {
             return NotFound("File not found");
         }
    }

    private Category GetCategoryById(IReadOnlyList<Category> catalog, string categoryId)
    {
        foreach (Category topCategory in catalog)
        {
            if (topCategory.LocalizedObject.Id == categoryId)
            {
                return topCategory;
            }

            foreach (Category subCategory in topCategory.SubCategories)
            {
                if (subCategory.LocalizedObject.Id == categoryId)
                {
                    return subCategory;
                }
            }
        }

        return null;
    }
}
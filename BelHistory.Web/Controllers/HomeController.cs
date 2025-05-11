using Microsoft.AspNetCore.Mvc;

namespace BelHistory.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
using Microsoft.AspNetCore.Mvc;

namespace ReportManager.Controllers;
public class HomeController : Controller
{
    public HomeController(){
    
    }
    public IActionResult Index()
    {
        return View();
    }
}
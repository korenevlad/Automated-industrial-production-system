using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;
using ReportManager.Models.Search;

namespace ReportManager.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public HomeController(IUnitOfWork unitOfWork)
    {
        
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        
        Console.WriteLine($"User Authenticated: {User.Identity?.IsAuthenticated}");
        Console.WriteLine($"User Name: {User.Identity?.Name}");

        if (!User.Identity?.IsAuthenticated ?? false)
        {
            return RedirectToAction("Login", "Account");
        }
        
        var model = new TechnologicalProcessSearch()
        {
            listOfTechnologicalProcess = _unitOfWork.TechnologicalProcessRepository.GetAll().ToList()
        };
        return View(model);
    }
}
using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;
using ReportManager.Models.Search;

namespace ReportManager.Controllers;
public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public HomeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        var model = new TechnologicalProcessSearch()
        {
            listOfTechnologicalProcess = _unitOfWork.TechnologicalProcessRepository.GetAll().ToList()
        };
        return View(model);
    }
}
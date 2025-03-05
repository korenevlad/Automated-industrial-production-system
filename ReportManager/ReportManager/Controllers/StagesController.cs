using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;
using ReportManager.Models.Search;

namespace ReportManager.Controllers;

public class StagesController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public StagesController(IUnitOfWork unitOfWork)
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
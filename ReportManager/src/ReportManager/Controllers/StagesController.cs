using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;

namespace ReportManager.Controllers;
[Authorize]
public class StagesController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public StagesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index(Guid id)
    {
        return View(id);
    }
}
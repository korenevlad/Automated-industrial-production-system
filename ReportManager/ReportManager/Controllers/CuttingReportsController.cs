using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;

namespace ReportManager.Controllers;

public class CuttingReportsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CuttingReportsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Parameters(Guid id)
    {
        var parametersCutting = _unitOfWork.ParametersCuttingArrayProcessRepository
            .GetAll().Where(p => p.cutting_array_process_id == id).ToList();
        return View(parametersCutting);
    }
    public IActionResult Errors(Guid id)
    {
        var parametersCutting = _unitOfWork.ParametersCuttingArrayProcessRepository
            .GetAll().Where(p => p.cutting_array_process_id == id 
                                 && (p.pressure_is_normal == false || p.speed_is_normal == false)).ToList();
        return View(parametersCutting);
    }
}
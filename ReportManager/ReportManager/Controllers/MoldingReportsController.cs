using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;

namespace ReportManager.Controllers;

public class MoldingReportsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public MoldingReportsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Parameters(Guid id)
    {
        var parametersMolding = _unitOfWork.ParametersMoldingAndInitialExposureProcessRepository
            .GetAll().Where(p => p.molding_and_initial_exposure_process_id == id).ToList();
        return View(parametersMolding);
    }
    public IActionResult Errors(Guid id)
    {
        var parametersMolding = _unitOfWork.ParametersMoldingAndInitialExposureProcessRepository
            .GetAll().Where(p => p.molding_and_initial_exposure_process_id == id 
                                 && (p.temperature_is_normal == false)).ToList();
        return View(parametersMolding);
    }
}
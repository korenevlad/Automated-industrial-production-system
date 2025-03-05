using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;

namespace ReportManager.Controllers;

public class MixingReportsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public MixingReportsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Parameters(Guid id)
    {
        var parametersMixing = _unitOfWork.ParametersMixingProcessRepository
            .GetAll().Where(p => p.mixing_process_id == id).ToList();
        return View(parametersMixing);
    }
    public IActionResult Errors(Guid id)
    {
        var parametersMixing = _unitOfWork.ParametersMixingProcessRepository
            .GetAll().Where(p => p.mixing_process_id == id 
                                 && (p.temperature_mixture_is_normal == false || p.mixing_speed_is_normal == false)).ToList();
        return View(parametersMixing);
    }
}
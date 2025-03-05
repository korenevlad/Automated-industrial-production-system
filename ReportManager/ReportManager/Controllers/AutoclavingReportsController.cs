using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;

namespace ReportManager.Controllers;

public class AutoclavingReportsController: Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public AutoclavingReportsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Parameters(Guid id)
    {
        var parametersAutoclaving = _unitOfWork.ParametersAutoclavingProcessRepository
            .GetAll().Where(p => p.autoclaving_process_id == id).ToList();
        return View(parametersAutoclaving);
    }
    public IActionResult Errors(Guid id)
    {
        var parametersCutting = _unitOfWork.ParametersAutoclavingProcessRepository
            .GetAll().Where(p => p.autoclaving_process_id == id 
                                 && (p.pressure_is_normal == false || p.temperature_is_normal == false)).ToList();
        return View(parametersCutting);
    }
}
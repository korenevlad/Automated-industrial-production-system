using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;
using ReportManager.Models.Search;

namespace ReportManager.Controllers;
[Authorize]
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
    
    public IActionResult GetAnaliticData(Guid id)
    {
        var parametersAutoclaving = _unitOfWork.ParametersAutoclavingProcessRepository
            .GetAll().Where(p => p.autoclaving_process_id == id).ToList();
        var cuttingingAnaliticData = new AutoclavingAnaliticData()
        {
            MediumTemperature = parametersAutoclaving.Average(p => p.temperature),
            MaxTemperature = parametersAutoclaving.Max(p => p.temperature),
            MinTemperature = parametersAutoclaving.Min(p => p.temperature),
            CountErrorsTemperature = parametersAutoclaving
                .Where(p => p.temperature_is_normal == false).Count(),
            MediumPressure = parametersAutoclaving.Average(p => p.pressure),
            MaxPressure = parametersAutoclaving.Max(p => p.pressure),
            MinPressure = parametersAutoclaving.Min(p => p.pressure),
            CountErrorsPressure = parametersAutoclaving
                .Where(p => p.pressure_is_normal == false).Count(),
            AverageOperatingTimeBeforeFailure =
                (parametersAutoclaving.Last().init_time - parametersAutoclaving.First().init_time)
                / parametersAutoclaving.Where(p => p.autoclaving_process_id == id
                                               && (p.temperature_is_normal == false || p.pressure_is_normal == false))
                    .Count(),
        };
        return View(cuttingingAnaliticData);
    }
}
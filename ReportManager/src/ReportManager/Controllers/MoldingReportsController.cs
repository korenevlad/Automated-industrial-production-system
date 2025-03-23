using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;
using ReportManager.Models.Search;

namespace ReportManager.Controllers;
[Authorize]
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
    
    public IActionResult GetAnaliticData(Guid id)
    {
        var parametersMolding = _unitOfWork.ParametersMoldingAndInitialExposureProcessRepository
            .GetAll().Where(p => p.molding_and_initial_exposure_process_id == id).ToList();
        var moldingAnaliticData = new MoldingAnaliticData()
        {
            MediumTemperature = parametersMolding.Average(p => p.temperature),
            MaxTemperature = parametersMolding.Max(p => p.temperature),
            MinTemperature = parametersMolding.Min(p => p.temperature),
            CountErrorsTemperature = parametersMolding
                .Where(p => p.temperature_is_normal == false).Count(),
            AverageOperatingTimeBeforeFailure = (parametersMolding.Last().init_time - parametersMolding.First().init_time) 
                                                / parametersMolding.Where(p => p.molding_and_initial_exposure_process_id== id 
                                                                              && (p.temperature_is_normal == false)).Count()
        };
        return View(moldingAnaliticData);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;
using ReportManager.Models.Search;

namespace ReportManager.Controllers;
[Authorize]
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

    public IActionResult GetAnaliticData(Guid id)
    {
        var parametersMixing = _unitOfWork.ParametersMixingProcessRepository
            .GetAll().Where(p => p.mixing_process_id == id).ToList();
        var mixingAnaliticData = new MixingAnaliticData()
        {
            MediumTemperatureMixture = parametersMixing.Average(p => p.temperature_mixture),
            MaxTemperatureMixture = parametersMixing.Max(p => p.temperature_mixture),
            MinTemperatureMixture = parametersMixing.Min(p => p.temperature_mixture),
            CountErrorsTemperatureMixture = parametersMixing
                .Where(p => p.temperature_mixture_is_normal == false).Count(),
            MediumSpeed = parametersMixing.Average(p => p.mixing_speed),
            MaxSpeed = parametersMixing.Max(p => p.mixing_speed),
            MinSpeed = parametersMixing.Min(p => p.mixing_speed),
            CountErrorsSpeed = parametersMixing
                .Where(p => p.mixing_speed_is_normal == false).Count(),
            AverageOperatingTimeBeforeFailure = (parametersMixing.Last().init_time - parametersMixing.First().init_time) 
                                                / parametersMixing.Where(p => p.mixing_process_id == id 
                                                                              && (p.temperature_mixture_is_normal == false || p.mixing_speed_is_normal == false)).Count()
        };
        return View(mixingAnaliticData);
    }
}
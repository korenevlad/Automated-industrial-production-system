using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;
using ReportManager.Models.Search;

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

    public IActionResult GetAnaliticData(Guid id)
    {
        var parametersCutting = _unitOfWork.ParametersCuttingArrayProcessRepository
            .GetAll().Where(p => p.cutting_array_process_id == id).ToList();
        var cuttingingAnaliticData = new CuttingAnaliticData()
        {
            MediumPressure = parametersCutting.Average(p => p.pressure),
            MaxPressure = parametersCutting.Max(p => p.pressure),
            MinPressure = parametersCutting.Min(p => p.pressure),
            CountErrorsPressure = parametersCutting
                .Where(p => p.pressure_is_normal == false).Count(),
            MediumSpeed = parametersCutting.Average(p => p.speed),
            MaxSpeed = parametersCutting.Max(p => p.speed),
            MinSpeed = parametersCutting.Min(p => p.speed),
            CountErrorsSpeed = parametersCutting
                .Where(p => p.speed_is_normal == false).Count(),
            AverageOperatingTimeBeforeFailure =
                (parametersCutting.Last().init_time - parametersCutting.First().init_time)
                / parametersCutting.Where(p => p.cutting_array_process_id == id
                                               && (p.pressure_is_normal == false || p.speed_is_normal == false))
                    .Count(),
        };
        return View(cuttingingAnaliticData);
    }
}
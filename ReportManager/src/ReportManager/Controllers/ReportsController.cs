using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportManager.DataAccess.Repository;

namespace ReportManager.Controllers;
[Authorize]
public class ReportsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ReportsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Mixing(Guid id)
    {
        var mixingProcess = _unitOfWork.MixingProcessRepository.GetAll()
            .FirstOrDefault(p => p.technological_process_id == id);
        return View(mixingProcess.mixing_process_id);
    }
    public IActionResult Molding(Guid id)
    {
        var moldingProcess = _unitOfWork.MoldingAndInitialExposureProcessRepository.GetAll()
            .FirstOrDefault(p => p.technological_process_id == id);
        return View(moldingProcess.molding_and_initial_exposure_process_id);
    }
    public IActionResult Cutting(Guid id)
    {
        var cuttingProcess = _unitOfWork.CuttingArrayProcessRepository.GetAll()
            .FirstOrDefault(p => p.technological_process_id == id);
        return View(cuttingProcess.cutting_array_process_id);
    }
    public IActionResult Autoclaving(Guid id)
    {
        var autoclavingProcess = _unitOfWork.AutoclavingProcessRepository.GetAll()
            .FirstOrDefault(p => p.technological_process_id == id);
        return View(autoclavingProcess.autoclaving_process_id);
    }
}
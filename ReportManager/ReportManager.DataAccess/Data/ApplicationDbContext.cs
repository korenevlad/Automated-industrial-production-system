using Microsoft.EntityFrameworkCore;
using ReportManager.Models;

namespace ReportManager.DataAccess.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    
    // тех процесс
    private DbSet<Technological_process> technological_processes { get; set; }
    
    // смешивание
    private DbSet<Mixing_process> mixing_process  { get; set; }
    private DbSet<Parameters_mixing_process> parameters_mixing_process {get; set; }
    
    // формование
    private DbSet<Molding_and_initial_exposure_process> molding_and_initial_exposure_process {get; set; }
    private DbSet<Parameters_molding_and_initial_exposure_process> parameters_molding_and_initial_exposure_process { get; set; }
    
    // резка массива
    private DbSet<Cutting_array_process> cutting_array_process { get; set; }
    private DbSet<Parameters_cutting_array_process> parameters_cutting_array_process { get; set; }
    
    // автоклавирование
    private DbSet<Autoclaving_process> autoclaving_process { get; set; } 
    private DbSet<Parameters_autoclaving_process> parameters_autoclaving_process { get; set; }
}
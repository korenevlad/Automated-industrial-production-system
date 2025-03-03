using KafkaConsumer.Models;
using Microsoft.EntityFrameworkCore;

namespace KafkaConsumer.DataAccess.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    
    private DbSet<Technological_process> technological_processes { get; set; }
    
    private DbSet<Mixing_process> mixing_process  { get; set; }
    private DbSet<Parameters_mixing_process> parameters_mixing_process {get; set; }
    
    private DbSet<Molding_and_initial_exposure_process> molding_and_initial_exposure_process {get; set; }
    private DbSet<Parameters_molding_and_initial_exposure_process> parameters_molding_and_initial_exposure_process { get; set; } 
    
}
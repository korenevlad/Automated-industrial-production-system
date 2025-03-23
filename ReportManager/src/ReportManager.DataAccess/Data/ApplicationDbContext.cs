using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReportManager.Models;

namespace ReportManager.DataAccess.Data;
public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Technological_process>().ToTable("technological_processes");
        builder.Entity<Mixing_process>().ToTable("mixing_process");
        builder.Entity<Parameters_mixing_process>().ToTable("parameters_mixing_process");
        builder.Entity<Molding_and_initial_exposure_process>().ToTable("molding_and_initial_exposure_process");
        builder.Entity<Parameters_molding_and_initial_exposure_process>().ToTable("parameters_molding_and_initial_exposure_process");
        builder.Entity<Cutting_array_process>().ToTable("cutting_array_process");
        builder.Entity<Parameters_cutting_array_process>().ToTable("parameters_cutting_array_process");
        builder.Entity<Autoclaving_process>().ToTable("autoclaving_process");
        builder.Entity<Parameters_autoclaving_process>().ToTable("parameters_autoclaving_process");

        builder.Entity<User>().ToTable("AspNetUsers");
        builder.Entity<IdentityRole>().ToTable("AspNetRoles");
        builder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
        builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");
    }
    
    // тех процесс
    public DbSet<Technological_process> technological_processes { get; set; }
    
    // смешивание
    public DbSet<Mixing_process> mixing_process  { get; set; }
    public DbSet<Parameters_mixing_process> parameters_mixing_process {get; set; }
    
    // формование
    public DbSet<Molding_and_initial_exposure_process> molding_and_initial_exposure_process {get; set; }
    public DbSet<Parameters_molding_and_initial_exposure_process> parameters_molding_and_initial_exposure_process { get; set; }
    
    // резка массива
    public DbSet<Cutting_array_process> cutting_array_process { get; set; }
    public DbSet<Parameters_cutting_array_process> parameters_cutting_array_process { get; set; }
    
    // автоклавирование
    public DbSet<Autoclaving_process> autoclaving_process { get; set; } 
    public DbSet<Parameters_autoclaving_process> parameters_autoclaving_process { get; set; }
}
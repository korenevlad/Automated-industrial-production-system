using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ReportManager.DataAccess.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=Automated-industrial-production-system;Username=postgres;Password=1234");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
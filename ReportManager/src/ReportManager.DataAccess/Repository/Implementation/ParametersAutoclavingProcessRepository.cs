using ReportManager.DataAccess.Data;
using ReportManager.Models;

namespace ReportManager.DataAccess.Repository.Implementation;
public class ParametersAutoclavingProcessRepository : Repository<Parameters_autoclaving_process>, IParametersAutoclavingProcessRepository
{
    private readonly ApplicationDbContext _db;
    public ParametersAutoclavingProcessRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}
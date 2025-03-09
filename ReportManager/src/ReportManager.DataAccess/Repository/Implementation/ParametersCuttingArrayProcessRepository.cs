using ReportManager.DataAccess.Data;
using ReportManager.Models;

namespace ReportManager.DataAccess.Repository.Implementation;
public class ParametersCuttingArrayProcessRepository : Repository<Parameters_cutting_array_process>, IParametersCuttingArrayProcessRepository
{
    private readonly ApplicationDbContext _db;
    public ParametersCuttingArrayProcessRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}
using ReportManager.DataAccess.Data;
using ReportManager.Models;

namespace ReportManager.DataAccess.Repository.Implementation;
public class MixingProcessRepository : Repository<Mixing_process>, IMixingProcessRepository
{
    private readonly ApplicationDbContext _db;
    public MixingProcessRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}
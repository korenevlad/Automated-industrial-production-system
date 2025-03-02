using KafkaConsumer.DataAccess.Data;
using KafkaConsumer.Models;

namespace KafkaConsumer.DataAccess.Repository.Implementation;
public class TechnologicalProcessRepository : Repository<Technological_process>, ITechnologicalProcessRepository
{
    private readonly ApplicationDbContext _db;
    public TechnologicalProcessRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}
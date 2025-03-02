using KafkaConsumer.DataAccess.Data;
using KafkaConsumer.Models;

namespace KafkaConsumer.DataAccess.Repository.Implementation;
public class MixingProcessRepository : Repository<Mixing_process>, IMixingProcessRepository
{
    private readonly ApplicationDbContext _db;
    public MixingProcessRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}
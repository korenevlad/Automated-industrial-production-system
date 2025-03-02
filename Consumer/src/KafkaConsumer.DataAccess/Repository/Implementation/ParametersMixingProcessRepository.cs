using KafkaConsumer.DataAccess.Data;
using KafkaConsumer.Models;

namespace KafkaConsumer.DataAccess.Repository.Implementation;
public class ParametersMixingProcessRepository : Repository<Parameters_mixing_process>, IParametersMixingProcessRepository
{
    private readonly ApplicationDbContext _db;
    public ParametersMixingProcessRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}
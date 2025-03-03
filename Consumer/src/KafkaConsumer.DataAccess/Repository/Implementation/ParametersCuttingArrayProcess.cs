using KafkaConsumer.DataAccess.Data;
using KafkaConsumer.Models;

namespace KafkaConsumer.DataAccess.Repository.Implementation;
public class ParametersCuttingArrayProcess : Repository<Parameters_cutting_array_process>, IParametersCuttingArrayProcess
{
    private readonly ApplicationDbContext _db;
    public ParametersCuttingArrayProcess(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}
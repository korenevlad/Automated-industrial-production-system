using KafkaConsumer.DataAccess.Data;
using KafkaConsumer.Models;

namespace KafkaConsumer.DataAccess.Repository.Implementation;

public class AutoclavingProcessRepository : Repository<Autoclaving_process>, IAutoclavingProcessRepository
{
    private readonly ApplicationDbContext _db;
    public AutoclavingProcessRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}
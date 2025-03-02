using KafkaConsumer.DataAccess.Data;

namespace KafkaConsumer.DataAccess.Repository.Implementation;
public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _db;
    public ITechnologicalProcessRepository TechnologicalProcessRepository { get; private set; }
    public IMixingProcessRepository MixingProcessRepository { get; }
    public IParametersMixingProcessRepository ParametersMixingProcessRepository { get; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        TechnologicalProcessRepository = new TechnologicalProcessRepository(_db);
        MixingProcessRepository = new MixingProcessRepository(db);
        ParametersMixingProcessRepository = new ParametersMixingProcessRepository(_db);
    }
    public void Save()
    {
        _db.SaveChanges();
    }
}
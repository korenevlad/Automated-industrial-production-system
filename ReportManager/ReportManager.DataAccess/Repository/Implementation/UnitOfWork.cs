using ReportManager.DataAccess.Data;

namespace ReportManager.DataAccess.Repository.Implementation;
public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _db;
    public ITechnologicalProcessRepository TechnologicalProcessRepository { get; private set; }
    public IMixingProcessRepository MixingProcessRepository { get; }
    public IParametersMixingProcessRepository ParametersMixingProcessRepository { get; }
    public IMoldingAndInitialExposureProcessRepository MoldingAndInitialExposureProcessRepository { get; }
    public IParametersMoldingAndInitialExposureProcessRepository ParametersMoldingAndInitialExposureProcessRepository { get; }
    public ICuttingArrayProcessRepository CuttingArrayProcessRepository { get; }
    public IParametersCuttingArrayProcessRepository ParametersCuttingArrayProcessRepository { get; }
    public IAutoclavingProcessRepository AutoclavingProcessRepository { get; }
    public IParametersAutoclavingProcessRepository ParametersAutoclavingProcessRepository { get; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        TechnologicalProcessRepository = new TechnologicalProcessRepository(_db);
        MixingProcessRepository = new MixingProcessRepository(_db);
        ParametersMixingProcessRepository = new ParametersMixingProcessRepository(_db);
        MoldingAndInitialExposureProcessRepository = new MoldingAndInitialExposureProcessRepository(_db);
        ParametersMoldingAndInitialExposureProcessRepository = new ParametersMoldingAndInitialExposureProcessRepository(_db);
        CuttingArrayProcessRepository = new CuttingArrayProcessRepository(_db);
        ParametersCuttingArrayProcessRepository = new ParametersCuttingArrayProcessRepository(_db);
        AutoclavingProcessRepository = new AutoclavingProcessRepository(_db);
        ParametersAutoclavingProcessRepository = new ParametersAutoclavingProcessRepository(_db);
    }
    public void Save()
    {
        _db.SaveChanges();
    }
}
namespace ReportManager.DataAccess.Repository;

public interface IUnitOfWork
{
    ITechnologicalProcessRepository TechnologicalProcessRepository { get; }
    IMixingProcessRepository MixingProcessRepository { get; }
    IParametersMixingProcessRepository ParametersMixingProcessRepository { get; }
    IMoldingAndInitialExposureProcessRepository MoldingAndInitialExposureProcessRepository { get; }
    IParametersMoldingAndInitialExposureProcessRepository ParametersMoldingAndInitialExposureProcessRepository { get; }
    ICuttingArrayProcessRepository CuttingArrayProcessRepository { get; }
    IParametersCuttingArrayProcessRepository ParametersCuttingArrayProcessRepository { get; }
    IAutoclavingProcessRepository AutoclavingProcessRepository { get; }
    IParametersAutoclavingProcessRepository ParametersAutoclavingProcessRepository { get; }
    void Save();
}
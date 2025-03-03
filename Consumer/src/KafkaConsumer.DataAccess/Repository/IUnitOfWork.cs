namespace KafkaConsumer.DataAccess.Repository;

public interface IUnitOfWork
{
    ITechnologicalProcessRepository TechnologicalProcessRepository { get; }
    IMixingProcessRepository MixingProcessRepository { get; }
    IParametersMixingProcessRepository ParametersMixingProcessRepository { get; }
    IMoldingAndInitialExposureProcessRepository MoldingAndInitialExposureProcessRepository { get; }
    IParametersMoldingAndInitialExposureProcessRepository ParametersMoldingAndInitialExposureProcessRepository { get; }
    ICuttingArrayProcessRepository CuttingArrayProcessRepository { get; }
    IParametersCuttingArrayProcess ParametersCuttingArrayProcess { get; }
    void Save();
}
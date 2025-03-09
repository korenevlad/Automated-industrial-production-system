namespace ReportManager.Models.Search;

public class MixingAnaliticData
{
    public double MediumTemperatureMixture { get; set; }
    public double MaxTemperatureMixture { get; set; }
    public double MinTemperatureMixture { get; set; }
    public double CountErrorsTemperatureMixture { get; set; }
    public double MediumSpeed { get; set; }
    public double MaxSpeed { get; set; }
    public double MinSpeed { get; set; }
    public double CountErrorsSpeed { get; set; }
    public TimeSpan AverageOperatingTimeBeforeFailure { get; set; }
    
}
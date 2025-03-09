namespace ReportManager.Models.Search;

public class MoldingAnaliticData
{
    public double MediumTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public double MinTemperature { get; set; }
    public double CountErrorsTemperature { get; set; }
    public TimeSpan AverageOperatingTimeBeforeFailure { get; set; }
}
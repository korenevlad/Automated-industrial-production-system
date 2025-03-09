namespace ReportManager.Models.Search;

public class AutoclavingAnaliticData
{
    public double MediumTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public double MinTemperature { get; set; }
    public double CountErrorsTemperature { get; set; }
    public double MediumPressure { get; set; }
    public double MaxPressure { get; set; }
    public double MinPressure { get; set; }
    public double CountErrorsPressure { get; set; }
    public TimeSpan AverageOperatingTimeBeforeFailure { get; set; }
}
namespace ReportManager.Models.Search;

public class CuttingAnaliticData
{
    public double MediumPressure { get; set; }
    public double MaxPressure { get; set; }
    public double MinPressure { get; set; }
    public double CountErrorsPressure { get; set; }
    public double MediumSpeed { get; set; }
    public double MaxSpeed { get; set; }
    public double MinSpeed { get; set; }
    public double CountErrorsSpeed { get; set; }
    public TimeSpan AverageOperatingTimeBeforeFailure { get; set; }
}
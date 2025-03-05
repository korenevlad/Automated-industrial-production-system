using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportManager.Models;

public class Parameters_autoclaving_process
{
    [Key]
    public Guid parameters_autoclaving_process_id { get; set; }
    
    public Guid autoclaving_process_id { get; set; }
    [ForeignKey("autoclaving_process_id")]
    public Autoclaving_process autoclaving_process_of_parameters { get; set; }
    
    public DateTime init_time { get; set; }
    public double temperature { get; set; }
    public bool temperature_is_normal { get; set; }
    public double pressure { get; set; }
    public bool pressure_is_normal { get; set; }
    public TimeSpan remaining_process_time { get; set; } 
}
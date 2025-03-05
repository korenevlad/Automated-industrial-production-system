using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportManager.Models;

public class Parameters_cutting_array_process
{
    [Key]
    public Guid parameters_cutting_array_process_id { get; set; }
    
    public Guid cutting_array_process_id { get; set; }
    [ForeignKey("cutting_array_process_id")]
    public Cutting_array_process cutting_array_process_of_parameters { get; set; }
    
    public DateTime init_time { get; set; }
    public double pressure { get; set; }
    public bool pressure_is_normal { get; set; }
    public double speed { get; set; }
    public bool speed_is_normal { get; set; }
}
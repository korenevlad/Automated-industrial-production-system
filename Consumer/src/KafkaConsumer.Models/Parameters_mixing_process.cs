using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KafkaConsumer.Models;
public class Parameters_mixing_process
{
    [Key]
    public Guid parameters_mixing_process_id { get; set; }
    
    public Guid mixing_process_id { get; set; }
    [ForeignKey("mixing_process_id")]
    public Mixing_process Mixing_process_of_parameters { get; set; }
    
    public DateTime init_time { get; set; }
    public double temperature_mixture { get; set; }
    public bool temperature_mixture_is_normal { get; set; }
    public double mixing_speed { get; set; }
    public bool mixing_speed_is_normal { get; set; }
    public TimeSpan remaining_process_time { get; set; } 
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KafkaConsumer.Models;
public class Parameters_molding_and_initial_exposure_process
{
    [Key]
    public Guid parameters_molding_and_initial_exposure_process_id { get; set; }
    
    public Guid molding_and_initial_exposure_process_id { get; set; }
    [ForeignKey("molding_and_initial_exposure_process_id")]
    public Molding_and_initial_exposure_process Molding_and_initial_exposure_process_of_parameters { get; set; }
    
    public DateTime init_time { get; set; }
    public double temperature { get; set; }
    public bool temperature_is_normal { get; set; }
    public TimeSpan remaining_process_time { get; set; } 
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportManager.Models;

public class Molding_and_initial_exposure_process
{
    [Key]
    public Guid molding_and_initial_exposure_process_id { get; set; }
    
    public Guid technological_process_id { get; set; }
    [ForeignKey("technological_process_id")]
    public Technological_process Technological_process_of_molding_process { get; set; }
    
    public DateTime date_start { get; set; }
    public DateTime date_end{ get; set; }
}
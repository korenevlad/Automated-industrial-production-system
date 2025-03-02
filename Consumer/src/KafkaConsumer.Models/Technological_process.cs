using System.ComponentModel.DataAnnotations;

namespace KafkaConsumer.Models;
public class Technological_process
{
    [Key]
    public Guid technological_process_id { get; set; }
    public DateTime date_start { get; set; }
    public DateTime date_end{ get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace RabbitMQ.Faculties.Entities
{
    public class Faculty
    {
        [Key]
        public long Id { get;set; }
        public string Name { get; set; }    
    }
}

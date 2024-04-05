namespace RabbitMQ.Faculties.Contracts.Events
{
    public class FacultyCreated: IEvent
    {
        public long Id { get; set; }    
        public string Name { get; set; }        
    }
}

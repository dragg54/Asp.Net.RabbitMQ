namespace RabbitMQ.Faculties.Contracts.Commands
{
    public class CreateFaculty: ICommand
    {
        public long Id { get; set; }    
        public string Name { get; set; }    
    }
}

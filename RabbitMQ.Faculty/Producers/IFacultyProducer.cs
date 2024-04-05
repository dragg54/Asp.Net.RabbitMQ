using RabbitMQ.Faculties.Contracts.Events;

namespace RabbitMQ.Faculties.Producers
{
    public interface IFacultyCreatedProducer: IProducer<FacultyCreated>
    {
        void Send(FacultyCreated message);
    }
}

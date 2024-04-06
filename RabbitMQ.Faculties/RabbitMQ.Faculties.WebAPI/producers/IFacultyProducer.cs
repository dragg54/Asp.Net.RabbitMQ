using RabbitMQ.Faculties.Contracts.Events;

namespace RabbitMQ.Faculties.WebAPI.Producers
{
    public interface IFacultyCreatedProducer: IProducer<FacultyCreated>
    {
        void Publish(FacultyCreated message);
    }
}

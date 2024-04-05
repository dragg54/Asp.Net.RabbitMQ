using RabbitMQ.Faculties.Contracts.Events;

namespace RabbitMQ.Faculties.Handlers
{
    public interface ICreateFacultyHandler: IHandleMessage<FacultyCreated>
    {
        void Handle(FacultyCreated message);
    }
}

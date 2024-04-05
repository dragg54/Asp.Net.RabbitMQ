using RabbitMQ.Faculties.Contracts;

namespace RabbitMQ.Faculties.Handlers
{
    public interface IHandleMessage<T>
    {
        void Handle(T message);
    }
}

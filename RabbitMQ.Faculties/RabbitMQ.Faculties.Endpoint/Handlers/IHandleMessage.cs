using RabbitMQ.Faculties.Endpoint;

namespace RabbitMQ.Faculties.Endpoint.Handlers
{
    public interface IHandleMessage<T>
    {
        void Handle(T message);
    }
}

namespace RabbitMQ.Faculties.Producers
{
    public interface IProducer<T>
    {
        public void Send(T message);    
    }
}

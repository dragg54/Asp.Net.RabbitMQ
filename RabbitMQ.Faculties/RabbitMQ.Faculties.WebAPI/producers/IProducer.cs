namespace RabbitMQ.Faculties.WebAPI.Producers
{
    public interface IProducer<T>
    {
        public void Publish(T message);    
    }
}

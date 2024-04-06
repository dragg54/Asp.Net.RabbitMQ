using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Faculties.Contracts.Events;
using System.Text;

namespace RabbitMQ.Faculties.WebAPI.Producers
{
    public class FacultyCreatedProducer: IFacultyCreatedProducer
    {
        private readonly ConnectionFactory connectionFactory;

        public FacultyCreatedProducer(ConnectionFactory connectionFactory)
        {
            this.connectionFactory= connectionFactory;
        }

        public void Publish(FacultyCreated message)
        {
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("faculty", exclusive: false);
            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "faculty", body: body);
        }
    }
}

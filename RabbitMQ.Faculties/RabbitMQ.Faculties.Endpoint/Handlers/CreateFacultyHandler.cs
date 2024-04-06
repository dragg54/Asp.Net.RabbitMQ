using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Faculties.Contracts.Events;
using RabbitMQ.Faculties.Endpoint.Data;
using RabbitMQ.Faculties.Entities;
using Serilog;
using System.Text;

namespace RabbitMQ.Faculties.Endpoint.Handlers
{
    public class CreateFacultyHandler: ICreateFacultyHandler
    {
        private readonly ConnectionFactory connectionFactory;

        public CreateFacultyHandler(ConnectionFactory connectionFactory)
        {
            connectionFactory = connectionFactory;
        }

        public void Handle()
        {
            using (var channel = connectionFactory.CreateConnection().CreateModel())
            {
                channel.QueueDeclare("faculty", exclusive: false);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                };
                channel.BasicConsume(queue: "queue_name", autoAck: true, consumer: consumer);
                Log.Information(consumer.ToString());
            }
        }
    }
}

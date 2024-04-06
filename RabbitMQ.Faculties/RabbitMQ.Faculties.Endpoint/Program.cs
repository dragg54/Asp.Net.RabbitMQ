using RabbitMQ.Client;
using RabbitMQ.Faculties.Endpoint.Bootstrapper;
using RabbitMQ.Faculties.Endpoint.Handlers;
using RabbitMQ.Faculties.Endpoint.Bootstrapper.ConfigurationSections;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client.Events;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//configure rabbitmq
//builder.Services.AddSingleton(sp =>
//{
//    var config = sp.GetRequiredService<IConfiguration>();
//    var rabbitConfig = config.GetSection(ConfigurationConstantNames.RabbitMqSection).Get<RabbitMQConfiguration>();
//    return new ConnectionFactory
//    {
//        HostName = rabbitConfig.Host,
//        Port = rabbitConfig.Port,
//        UserName = rabbitConfig.Username,
//        Password = rabbitConfig.Password,
//        VirtualHost = rabbitConfig.VirtualHost
//    };
//});
var rabbitConfig = builder.Configuration.GetSection(ConfigurationConstantNames.RabbitMqSection).Get<RabbitMQConfiguration>();

var connectionFactory = new ConnectionFactory
{
    HostName = rabbitConfig.Host,
    Port = rabbitConfig.Port,
    UserName = rabbitConfig.Username,
    Password = rabbitConfig.Password,
    VirtualHost = rabbitConfig.VirtualHost
};
var channel = connectionFactory.CreateConnection().CreateModel();

channel.QueueDeclare("faculty", exclusive: false);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
};
channel.BasicConsume(queue: "faculty", autoAck: true, consumer: consumer);
Log.Information(consumer.ToString());

//builder.Services.AddScoped<ICreateFacultyHandler, CreateFacultyHandler>();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync($"Cognitive Labs Co. (c) {DateTime.Now.Year}. This is the Paycurra Wallets Events Endpoint Service.");
    });
});
app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();

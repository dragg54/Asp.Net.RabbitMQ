using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Faculties.WebAPI.Bootstrapper;
using RabbitMQ.Faculties.WebAPI.Bootstrapper.ConfigurationSections;
using RabbitMQ.Faculties.Data;
using RabbitMQ.Faculties.WebAPI.Producers;
using RabbitMQ.Faculties.WebAPI.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configure rabbitmq
builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var rabbitConfig = config.GetSection(ConfigurationConstantNames.RabbitMqSection).Get<RabbitMQConfiguration>();
    return new ConnectionFactory
    {
        HostName = rabbitConfig.Host,
        Port = rabbitConfig.Port,
        UserName = rabbitConfig.Username,
        Password = rabbitConfig.Password,
        VirtualHost = rabbitConfig.VirtualHost
    };
});

builder.Services.AddDbContext<FacultyDbContext>(options =>
{
    Log.Information($"Using {builder.Environment.EnvironmentName} DB");
    var connectionString = builder.Configuration.GetConnectionString("DbConnection");
    if (builder.Environment.IsDevelopment())
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
    else
    {
        connectionString = builder.Configuration.GetConnectionString("SQLConnection");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
});

builder.Services.AddScoped<IFacultyService, FacultyService>();
builder.Services.AddDbContext<FacultyDbContext>();
builder.Services.AddScoped<IFacultyCreatedProducer, FacultyCreatedProducer>();



builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using RabbitMQ.Faculties.WebAPI.Extensions;
using RabbitMQ.Faculties.WebAPI.Producers;
using RabbitMQ.Faculties.WebAPI.Requests;
using System.Net;

namespace RabbitMQ.Faculties.WebAPI.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IFacultyCreatedProducer eventBus;
        public FacultyService( IFacultyCreatedProducer eventBus)
        {
            this.eventBus = eventBus;
        }

        public void CreateFaculty(PostFacultyRequest request)
        {
            try
            {
                var faculty = request.ToFacultyCreatedEvent();
                eventBus.Publish(faculty);
            }
            catch (InvalidOperationException ex)
            {
                var msg = $"Unable to create faculty, {ex.Message}";
                throw new HttpRequestException(msg, ex.InnerException, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                var msg = $"Unable to create faculty, {ex.Message}";
                throw new HttpRequestException(msg, ex.InnerException, HttpStatusCode.InternalServerError);
            }
        }
    }
}

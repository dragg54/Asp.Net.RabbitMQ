using RabbitMQ.Faculties.Contracts.Events;
using RabbitMQ.Faculties.Entities;
using RabbitMQ.Faculties.WebAPI.Requests;

namespace RabbitMQ.Faculties.WebAPI.Extensions
{
    public static class Extensions
    {
        public static Faculty ToFaculty(this PostFacultyRequest request)
        {
            return new Faculty
            {
                Name = request.Name,
            };
        }

        public static FacultyCreated ToFacultyCreatedEvent(this PostFacultyRequest request)
        {
            return new FacultyCreated
            {
                Name = request.Name,
            };
        }
    }
}


using RabbitMQ.Faculties.WebAPI.Requests;

namespace RabbitMQ.Faculties.WebAPI.Services
{
    public interface IFacultyService
    {
        void CreateFaculty(PostFacultyRequest request);
    }
}

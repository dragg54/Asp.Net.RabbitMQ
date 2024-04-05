using RabbitMQ.Faculties.Handlers;
using RabbitMQ.Faculties.Requests;

namespace RabbitMQ.Faculties.Services
{
    public class FacultyService: IFacultyService
    {
        private readonly ICreateFacultyHandler createFacultyHandler;
        public FacultyService(ICreateFacultyHandler createFacultyHandler)
        {
            this.createFacultyHandler = createFacultyHandler;
        }

        public void CreateFaculty(PostFacultyRequest request)
        {
            try
            {
                createFacultyHandler.Handle()
            }
        }
    }
}

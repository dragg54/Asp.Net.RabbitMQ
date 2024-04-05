using RabbitMQ.Client;
using RabbitMQ.Faculties.Contracts.Events;
using RabbitMQ.Faculties.Data;
using RabbitMQ.Faculties.Entities;
using System.Text;

namespace RabbitMQ.Faculties.Handlers
{
    public class CreateFacultyHandler: ICreateFacultyHandler
    {
        private readonly FacultyDbContext facultyDbContext;

        public CreateFacultyHandler(FacultyDbContext facultyDbContext)
        {
            this.facultyDbContext = facultyDbContext;
        }

        public void Handle(FacultyCreated message)
        {
            var newFaculty = new Faculty
            {
                Id = message.Id,
                Name = message.Name
            };
            facultyDbContext.Faculty.Add(newFaculty);
            facultyDbContext.SaveChangesAsync(); 
        }
    }
}

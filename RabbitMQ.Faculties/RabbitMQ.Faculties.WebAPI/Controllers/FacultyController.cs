using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Faculties.WebAPI.Requests;
using RabbitMQ.Faculties.WebAPI.Services;

namespace RabbitMQ.Faculties.WebAPI.Controllers
{
    
    [ApiController]
    [Route("/api/[controller]")]
    public class FacultyController: ControllerBase
    {
        private readonly IFacultyService facultyService;
        
        public FacultyController(IFacultyService facultyService)
        {
            this.facultyService = facultyService;
        }

        [HttpPost]
        public async Task CreateFaculty(PostFacultyRequest request)
        {
            facultyService.CreateFaculty(request);
        }

    }
}

using Microsoft.EntityFrameworkCore;
using RabbitMQ.Faculties.Entities;

namespace RabbitMQ.Faculties.Endpoint
    .Data
{
    public class FacultyDbContext: DbContext
    {
        public FacultyDbContext(DbContextOptions<FacultyDbContext> options) : base(options)
        {

        }

        public DbSet<Faculty> Faculty { get; set; }
    }
}

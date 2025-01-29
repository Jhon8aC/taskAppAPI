using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; }
    }
}

using Core.Entities;
using Core.Interfaces;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;


namespace Infraestructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Asynchronously adds a new task to the database
        public async Task AddAsync(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
        }
        // Asynchronously saves changes to the database (commits any pending operations)
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        // Asynchronously retrieves all tasks from the database
        public async Task <List<TaskEntity>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }
    }
}

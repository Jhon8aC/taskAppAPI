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
        // Asynchronously retrieves all tasks from the database
        public async Task <List<TaskEntity>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        // Asynchronously retrieves task by id from the database
        public async Task<TaskEntity> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        // Asynchronously adds a new task to the database
        public async Task AddAsync(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
        }
        // Asynchronously adds a new task to the database
        public async Task UpdateAsync(TaskEntity task)
        {
             _context.Tasks.Update(task);
        }
        // Asynchronously delete a task by id to the database
        public async Task DeleteAsync(TaskEntity task)
        {
            _context.Tasks.Remove(task);
        }

        // Asynchronously saves changes to the database (commits any pending operations)
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

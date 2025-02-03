using Core.Entities;

namespace Core.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskEntity>> GetAllAsync();
        Task <TaskEntity> GetByIdAsync(Guid id);
        Task AddAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
        Task DeleteAsync(TaskEntity task);
        Task SaveChangesAsync();
        
    }
}


using Core.Entities;

namespace Core.Interfaces
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskEntity task);
        Task SaveChangesAsync();
        Task<List<TaskEntity>> GetAllAsync();
    }
}


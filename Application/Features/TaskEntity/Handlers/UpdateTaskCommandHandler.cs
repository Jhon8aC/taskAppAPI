using Application.Features.TaskEntity.Commands;
using AutoMapper;
using Core.Interfaces;
using MediatR;

namespace Application.Features.TaskEntity.Handlers
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Guid> 
    {
        private readonly ITaskRepository _taskRepository;
        public UpdateTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        // Handle method to process the UpdateTaskCommand request
        // It uptades an existing task and saves it to the repository, then returns the task ID
        public async Task<Guid> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            // Fetch the existing task by ID
            var task = await _taskRepository.GetByIdAsync(request.TaskId);

            // If the task doesn't exist, throw an exception
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {request.TaskId} not found.");
            }

            // Update only the fields that are not null and always the modified
            if (request.Title != null) task.Title = request.Title;
            if (request.Description != null) task.Description = request.Description;
            if (request.Completed.HasValue) task.Completed = request.Completed.Value;
            task.LastModified = DateTime.UtcNow;

            // Save the updated task to the repository and return the task id
            await _taskRepository.UpdateAsync(task);
            await _taskRepository.SaveChangesAsync();
            return task.ID;
        }

    }
}

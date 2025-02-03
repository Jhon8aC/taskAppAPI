using Application.Features.TaskEntity.Queries;
using Core.Interfaces;
using MediatR;

namespace Application.Features.TaskEntity.Handlers 
{ 
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Core.Entities.TaskEntity>
    {
        private readonly ITaskRepository _taskRepository;
        public GetTaskByIdQueryHandler(ITaskRepository taskrepository)
        {
            _taskRepository = taskrepository;
        }
        // Handle method to process the GetTaskByIdQuery request
        // It retrieves the task entity from the repository
        public async Task<Core.Entities.TaskEntity> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            // Fetch the existing task by ID
            var task = await _taskRepository.GetByIdAsync(request.TaskId);

            // If the task doesn't exist, throw an exception
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {request.TaskId} not found");
            }
            // Return the task id
            return task;
        }
    }
}

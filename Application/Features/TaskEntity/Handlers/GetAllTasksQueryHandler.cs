using Application.Features.TaskEntity.Queries;
using Core.Interfaces;
using MediatR;

namespace Application.Features.TaskEntity.Handlers
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, List<Core.Entities.TaskEntity>>
    {
        private readonly ITaskRepository _taskRepository;
        public GetAllTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        // Handle method to process the GetAllTasksQuery request
        // It retrieves all tasks from the repository and returns them as a list
        public async Task<List<Core.Entities.TaskEntity>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetAllAsync();
            // If no tasks are found, return an empty list
            return task ?? new List<Core.Entities.TaskEntity>();
        }
    }
}

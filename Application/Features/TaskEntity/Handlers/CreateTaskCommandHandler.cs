using Application.Features.TaskEntity.Commands;
using Core.Interfaces;
using MediatR;

namespace Application.Features.TaskEntity.Handlers
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand,Guid>
    {
        private readonly ITaskRepository _taskRepository;
        public CreateTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        // Handle method to process the CreateTaskCommand request
        // It creates a new task and saves it to the repository, then returns the task ID
        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new Application.Exceptions.ValidationException(new List<string> { $"{nameof(request)} cannot be null." });
            }
            // Creating a new TaskEntity object from the request data
            var task = new Core.Entities.TaskEntity
            {
                ID = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Completed = request.Completed,
                CreationDate = DateTime.UtcNow,
            };
            // Methods of ITaskRepository(TaskRepository)
            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();
            // Returning the ID of the created task
            return task.ID;
        }
    }
}

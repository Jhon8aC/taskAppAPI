using Application.Features.TaskEntity.Commands;
using Core.Interfaces;
using MediatR;

namespace Application.Features.TaskEntity.Handlers
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand,Guid>
    {
        private readonly ITaskRepository _taskRepository;
        public DeleteTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task <Guid> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new Application.Exceptions.ValidationException(new List<string> { $"{nameof(request)} cannot be null." });
            }

            var task = await _taskRepository.GetByIdAsync(request.TaskId);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {request.TaskId} not found");
            }
            await _taskRepository.DeleteAsync(task);
            await _taskRepository.SaveChangesAsync();
            return request.TaskId;
        }
    }
}

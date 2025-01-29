using MediatR;

namespace Application.Features.TaskEntity.Commands
{
    public class CreateTaskCommand : IRequest<Guid>
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required bool Completed { get; set; }
    }
}

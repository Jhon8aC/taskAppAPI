using MediatR;

namespace Application.Features.TaskEntity.Commands
{
    public class DeleteTaskCommand : IRequest<Guid>
    {
        public Guid TaskId { get; set; }

    }
}

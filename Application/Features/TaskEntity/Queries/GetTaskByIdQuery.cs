using MediatR;

namespace Application.Features.TaskEntity.Queries
{
    public class GetTaskByIdQuery : IRequest<Core.Entities.TaskEntity>
    {
        public Guid TaskId { get; set; }
    }
}

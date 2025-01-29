using MediatR;

namespace Application.Features.TaskEntity.Queries
{
    public class GetAllTasksQuery : IRequest<List<Core.Entities.TaskEntity>> { }

}

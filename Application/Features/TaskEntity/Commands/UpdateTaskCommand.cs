using MediatR;

namespace Application.Features.TaskEntity.Commands
{
    public class UpdateTaskCommand : IRequest<Guid>
    {
        public Guid TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? Completed { get; set; }
        public DateTime? LastModified {  get; set; }

    }
}

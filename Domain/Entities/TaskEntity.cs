using Core.Common;

namespace Core.Entities
{
    public class TaskEntity : AuditableBaseEntity
    {
        public Guid ID { get; set; }
        public required string Title {  get; set; }
        public required string Description { get; set; }
        public required bool Completed { get; set; }
    }
}

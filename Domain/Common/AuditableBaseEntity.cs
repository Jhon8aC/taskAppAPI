
namespace Core.Common
{
    public abstract class AuditableBaseEntity
    {
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModified { get; set; }

    }
}

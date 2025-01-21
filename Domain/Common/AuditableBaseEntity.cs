using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public abstract class AuditableBaseEntity
    {
        public DateTime CreationDate { get; set; }
        public DateTime? LastModified { get; set; }

    }
}

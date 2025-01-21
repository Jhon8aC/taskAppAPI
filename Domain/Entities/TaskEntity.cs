using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TaskEntity : AuditableBaseEntity
    {
        public Guid ID { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}

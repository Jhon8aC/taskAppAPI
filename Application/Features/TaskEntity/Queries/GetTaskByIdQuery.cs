using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaskEntity.Queries
{
    public class GetTaskByIdQuery : IRequest<Core.Entities.TaskEntity>
    {
        public Guid TaskId { get; set; }
    }
}

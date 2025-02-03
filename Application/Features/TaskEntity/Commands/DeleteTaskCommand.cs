using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaskEntity.Commands
{
    public class DeleteTaskCommand : IRequest<Guid>
    {
        public Guid TaskId { get; set; }

    }
}

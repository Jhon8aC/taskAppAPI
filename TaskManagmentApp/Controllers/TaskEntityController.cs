using Application.Features.TaskEntity.Commands;
using Application.Features.TaskEntity.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagmentApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskEntityController : ControllerBase
    {
        public readonly IMediator _mediator;
        public TaskEntityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new {taskId = id});
        }
        [HttpGet]
        public async Task<IActionResult> getAllTask()
        {
            var tasks = await _mediator.Send(new GetAllTasksQuery());
            return Ok(tasks);
        }
    }
}

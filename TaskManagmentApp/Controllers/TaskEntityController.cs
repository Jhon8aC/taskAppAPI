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
        private readonly IMediator _mediator;
        public TaskEntityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _mediator.Send(new GetAllTasksQuery());
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var task = await _mediator.Send(new GetTaskByIdQuery { TaskId = id });
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            var taskId = await _mediator.Send(command);
            return Ok(new { taskId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskCommand command)
        {
            command.TaskId = id;
            var taskId = await _mediator.Send(command);
            return Ok(new { taskId });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var taskId = await _mediator.Send(new DeleteTaskCommand { TaskId = id });
            return Ok(new { taskId });
        }

    }
}

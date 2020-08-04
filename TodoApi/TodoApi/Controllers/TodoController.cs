using System;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Todo.Api.Controllers.Command;
using Todo.Api.Controllers.Query;

namespace Todo.Api.Controllers
{
    [Route("api/todo"), ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IMediator mediator;

        public TodoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(RegisterNewTaskResponse))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> RegisterNewTask([FromBody] RegisterNewTaskCommand command)
        {
            var response = await mediator.Send(command);
            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }

            return BadRequest(response.Error);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(GetTaskByIdResponse))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> GetTaskById([FromQuery] Guid taskId)
        {
            var response = await mediator.Send(new GetTaskByIdQuery { TaskId = taskId });
            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }

            return NotFound(response.Error);
        }
    }
}

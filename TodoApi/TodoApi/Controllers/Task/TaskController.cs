using System;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Todo.Api.Controllers.Task.DeleteTask;
using Todo.Api.Controllers.Task.GetById;
using Todo.Api.Controllers.Task.RegisterNew;
using Todo.Api.Controllers.Task.UpdateTask;

namespace Todo.Api.Controllers.Task
{
    [Route("api/todo"), ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator mediator;

        public TaskController(IMediator mediator)
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

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500, Type = typeof(string))]
        //[ProducesResponseType(451)]  ?xD
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand command)
        {
            var response = await mediator.Send(command);
            if (response.IsSuccess)
            {
                return Ok();
            }

            return StatusCode(500, response.Error);
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(500, Type = typeof(string))]
        public async Task<IActionResult> DeleteTask([FromQuery] Guid taskId)
        {
            var response = await mediator.Send(new DeleteTaskCommand { TaskId = taskId });
            if (response.IsSuccess)
            {
                return Ok();
            }

            return StatusCode(500, response.Error);
        }

    }
}

using CSharpFunctionalExtensions;
using MediatR;
using System;

namespace Todo.Api.Controllers.Task.DeleteTask
{
    public sealed class DeleteTaskCommand : IRequest<Result>
    {
        public DeleteTaskCommand()
        {

        }

        public DeleteTaskCommand(Guid taskId)
        {
            TaskId = taskId;
        }

        public Guid TaskId { get; set;  }
    }
}
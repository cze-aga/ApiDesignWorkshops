using System;

using CSharpFunctionalExtensions;

using MediatR;

namespace Todo.Api.Controllers.Task.UpdateTask
{
    public sealed class UpdateTaskCommand : IRequest<Result>
    {
        private UpdateTaskCommand()
        {
        }

        public UpdateTaskCommand(
            Guid taskId,
            string updatedName,
            string updatedDescription)
        {
            TaskId = taskId;
            UpdatedName = updatedName;
            UpdatedDescription = updatedDescription;
        }

        public Guid TaskId { get; set; }

        public string UpdatedName { get; set; }

        public string UpdatedDescription { get; set; }
    }
}

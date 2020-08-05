using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using MediatR;

namespace Todo.Api.Controllers.Task.UpdateTask
{
    public sealed class UpdateTaskCommand : IRequest<Result>
    {
        //bez tego jsonserializer zeswiruje
        public UpdateTaskCommand()
        {

        }
        public UpdateTaskCommand(Guid taskId, string name, string description)
        {
            TaskId = taskId;
            UpdatedName = name;
            UpdatedDescription = description;
        }

        public Guid TaskId { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedDescription { get; set; }
    }
}

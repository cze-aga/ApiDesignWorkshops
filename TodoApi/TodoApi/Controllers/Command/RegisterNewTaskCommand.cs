using CSharpFunctionalExtensions;

using MediatR;

namespace Todo.Api.Controllers.Command
{
    public sealed class RegisterNewTaskCommand : IRequest<Result<RegisterNewTaskResponse>>
    {
        private RegisterNewTaskCommand()
        {
        }

        public RegisterNewTaskCommand(string taskName, string taskDescription)
        {
            TaskName = taskName;
            TaskDescription = taskDescription;
        }

        public string TaskName { get; set;  }

        public string TaskDescription { get; set;  }
    }
}

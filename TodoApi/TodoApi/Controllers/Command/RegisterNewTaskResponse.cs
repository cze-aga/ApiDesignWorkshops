using System;

namespace Todo.Api.Controllers.Command
{
    [Serializable]
    public sealed class RegisterNewTaskResponse
    {
        private RegisterNewTaskResponse()
        {
        }

        public RegisterNewTaskResponse(Guid taskId)
        {
            TaskId = taskId;
        }

        public Guid TaskId { get; set; }
    }
}

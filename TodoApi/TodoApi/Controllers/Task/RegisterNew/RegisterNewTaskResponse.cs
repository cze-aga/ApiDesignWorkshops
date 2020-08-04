using System;

namespace Todo.Api.Controllers.Task.RegisterNew
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

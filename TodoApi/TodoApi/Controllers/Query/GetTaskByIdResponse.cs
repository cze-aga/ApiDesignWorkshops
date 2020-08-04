using System;

namespace Todo.Api.Controllers.Query
{
    [Serializable]
    public class GetTaskByIdResponse
    {
        private GetTaskByIdResponse()
        {
        }

        public GetTaskByIdResponse(
            Guid taskId,
            string name,
            string description)
        {
            TaskId = taskId;
            Name = name;
            Description = description;
        }

        public Guid TaskId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

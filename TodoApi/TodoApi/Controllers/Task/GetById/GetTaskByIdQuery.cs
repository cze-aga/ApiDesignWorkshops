using System;

using CSharpFunctionalExtensions;

using MediatR;

namespace Todo.Api.Controllers.Task.GetById
{
    public class GetTaskByIdQuery : IRequest<Result<GetTaskByIdResponse>>
    {
        public Guid TaskId { get; set; }
    }
}

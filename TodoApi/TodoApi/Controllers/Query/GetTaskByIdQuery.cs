using System;

using CSharpFunctionalExtensions;

using MediatR;

namespace Todo.Api.Controllers.Query
{
    public class GetTaskByIdQuery : IRequest<Result<GetTaskByIdResponse>>
    {
        public Guid TaskId { get; set; }
    }
}

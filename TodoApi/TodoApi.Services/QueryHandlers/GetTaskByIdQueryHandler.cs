using System;
using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Microsoft.EntityFrameworkCore;

using Todo.Api.Controllers.Task.GetById;
using Todo.Common.ServiceContracts;
using Todo.Infrastructure.DatabaseContext;

namespace Todo.Services.QueryHandlers
{
    internal class GetTaskByIdQueryHandler : IValidatedRequestHandler<GetTaskByIdQuery, Result<GetTaskByIdResponse>>
    {
        private readonly BaseTodoDbContext dbContext;

        public GetTaskByIdQueryHandler(DbContext dbContext)
        {
            if (dbContext is BaseTodoDbContext todoDbContext)
            {
                this.dbContext = todoDbContext;
            }
            else
            {
                throw new ArgumentException(nameof(dbContext));
            }
        }

        public async Task<Result<GetTaskByIdResponse>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await dbContext.Tasks.FindAsync(request.TaskId);

            return task is null ?
                Result.Failure<GetTaskByIdResponse>($"Task with Id {request.TaskId} was not found!") :
                Result.Success(new GetTaskByIdResponse(task.Id, task.Name, task.Description));
        }
    }
}

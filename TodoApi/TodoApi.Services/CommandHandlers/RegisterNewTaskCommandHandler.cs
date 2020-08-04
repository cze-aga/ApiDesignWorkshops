using System;
using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Microsoft.EntityFrameworkCore;

using Todo.Api.Controllers.Command;
using Todo.Common.ServiceContracts;
using Todo.Infrastructure.DatabaseContext;

namespace Todo.Services.CommandHandlers
{
    internal class RegisterNewTaskCommandHandler : IValidatedRequestHandler<RegisterNewTaskCommand, Result<RegisterNewTaskResponse>>
    {
        private readonly BaseTodoDbContext dbContext;

        public RegisterNewTaskCommandHandler(DbContext dbContext)
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

        public async Task<Result<RegisterNewTaskResponse>> Handle(RegisterNewTaskCommand request, CancellationToken cancellationToken)
        {
            var taskToRegister = new Model.Task
            {
                Name = request.TaskName,
                Description = request.TaskDescription
            };

            dbContext.Tasks.Add(taskToRegister);

            _ = await dbContext.SaveChangesAsync();

            return Result.Success(new RegisterNewTaskResponse(taskToRegister.Id));
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Microsoft.EntityFrameworkCore;

using Todo.Api.Controllers.Task.UpdateTask;
using Todo.Common.ServiceContracts;
using Todo.Infrastructure.DatabaseContext;

namespace Todo.Services.CommandHandlers
{
    internal class UpdateTaskCommandHandler : IValidatedRequestHandler<UpdateTaskCommand, Result>
    {
        private readonly BaseTodoDbContext todoDbContext;

        public UpdateTaskCommandHandler(DbContext context)
        {
            if (context is BaseTodoDbContext todoDbContext)
            {
                this.todoDbContext = todoDbContext;
            }
            else
            {
                throw new ArgumentException(nameof(context));
            }
        }

        public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingTask = todoDbContext.Tasks.Find(request.TaskId);

                existingTask.Name = request.UpdatedName;
                existingTask.Description = request.UpdatedDescription;

                todoDbContext.Tasks.Update(existingTask);
                await todoDbContext.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}

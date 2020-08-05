using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Api.Controllers.Task.DeleteTask;
using Todo.Common.ServiceContracts;
using Todo.Infrastructure.DatabaseContext;

namespace Todo.Services.CommandHandlers
{
    internal class DeleteTaskCommandHandler : IValidatedRequestHandler<DeleteTaskCommand, Result>
    {
        private readonly BaseTodoDbContext todoContext;

        public DeleteTaskCommandHandler(DbContext context)
        {
            if (context is BaseTodoDbContext todoDbContext)
            {
                this.todoContext = todoDbContext;
            }
            else
            {
                throw new ArgumentException(nameof(context));
            }
        }

        public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingTask = todoContext.Tasks.Find(request.TaskId);

                todoContext.Tasks.Remove(existingTask);
                await todoContext.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Process of deleting entity failed: {ex.Message}");
            }
        }
    }
}

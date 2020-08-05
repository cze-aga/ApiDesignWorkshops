using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Api.Controllers.Task.UpdateTask;
using Todo.Common.ServiceContracts;
using Todo.Infrastructure.DatabaseContext;

namespace Todo.Services.CommandHandlers
{
    internal class UpdateTaskCommandHandler : IValidatedRequestHandler<UpdateTaskCommand, Result>
    {
        private readonly BaseTodoDbContext todoContext;
        
        public UpdateTaskCommandHandler(DbContext context)
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

        public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingTask = todoContext.Tasks.Find(request.TaskId);

                existingTask.Name = request.UpdatedName;
                existingTask.Description = request.UpdatedDescription;

                todoContext.Tasks.Update(existingTask);
                await todoContext.SaveChangesAsync();
                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}

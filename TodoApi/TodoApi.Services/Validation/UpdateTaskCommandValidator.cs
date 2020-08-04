using System;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.EntityFrameworkCore;

using Todo.Api.Controllers.Task.UpdateTask;
using Todo.Infrastructure.DatabaseContext;

namespace Todo.Services.Validation
{
    internal class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator(DbContext context)
        {
            if (context is BaseTodoDbContext todoDbContext)
            {
                RuleFor(command => command.TaskId).NotNull().NotEmpty();
                RuleFor(command => command.TaskId)
                    .Custom((id, context) =>
                    {
                        if (todoDbContext.Tasks.Find(id) == null)
                        {
                            context.AddFailure(new ValidationFailure(nameof(UpdateTaskCommand.TaskId), $"Task with Id {id} does not exist!"));
                        }
                    });
                RuleFor(command => command.UpdatedName).NotNull().NotEmpty();
            }
            else
            {
                throw new ArgumentException(nameof(context));
            }
        }
    }
}

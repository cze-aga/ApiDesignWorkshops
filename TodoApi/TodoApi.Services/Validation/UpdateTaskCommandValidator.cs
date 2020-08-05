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
        //walidowanie requestow ktore leca od uzytkownika- czy dobrze wpisuje, jak nei to rzucamy 400
        public UpdateTaskCommandValidator(DbContext context)
        {
            if (context is BaseTodoDbContext todoDbContext)
            {
                RuleFor(command => command.TaskId).Custom((id, context) =>
                {
                    if (todoDbContext.Tasks.Find(id) == null)
                    {
                        context.AddFailure(new ValidationFailure(nameof(UpdateTaskCommand.TaskId), $"{id}not exists"));
                    }
                });

                RuleFor(command => command.TaskId).NotNull().NotEmpty();
                RuleFor(command => command.UpdatedName).NotNull().NotEmpty();
            }
            else
            {
                throw new ArgumentException(nameof(context));
            }
        }
    }
}

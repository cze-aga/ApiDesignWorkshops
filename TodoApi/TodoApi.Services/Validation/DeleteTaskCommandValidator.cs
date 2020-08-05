using FluentValidation;
using Todo.Api.Controllers.Task.DeleteTask;

namespace Todo.Services.Validation
{
    internal class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskCommandValidator()
        {
            RuleFor(deleteCommand => deleteCommand.TaskId).NotNull().NotEmpty();
        }
    }
}

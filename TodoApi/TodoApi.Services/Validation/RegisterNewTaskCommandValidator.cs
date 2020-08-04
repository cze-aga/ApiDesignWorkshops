using FluentValidation;

using Todo.Api.Controllers.Task.RegisterNew;

namespace Todo.Services.Validation
{
    internal class RegisterNewTaskCommandValidator : AbstractValidator<RegisterNewTaskCommand>
    {
        public RegisterNewTaskCommandValidator()
        {
            RuleFor(command => command.TaskName).NotNull().NotEmpty();
        }
    }
}

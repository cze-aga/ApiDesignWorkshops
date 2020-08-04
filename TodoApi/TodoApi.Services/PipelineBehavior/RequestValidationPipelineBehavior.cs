using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using MediatR;

namespace Todo.Services.PipelineBehavior
{
    internal class RequestValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IList<IValidator<TRequest>> validators;

        public RequestValidationPipelineBehavior(IList<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = validators.Select(v => v.Validate(context)).SelectMany(vr => vr.Errors)
                .Where(ve => ve != null).Select(ve => ve.ErrorMessage).ToList();

            if (failures.Any())
            {
                throw new ValidationException(string.Join(", ", failures));
            }

            return next();
        }
    }
}
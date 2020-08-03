using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace TodoApi.Services.PipelineBehavior
{
    internal class RequestValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IList<IValidator<TRequest>> validators;

        public RequestValidationPipelineBehavior(IList<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = this.validators.Select(v => v.Validate(context))
                .SelectMany(vr => vr.Errors)
                .Where(ve => ve != null)
                .Select(ve => ve.ErrorMessage)
                .ToList();

            return failures.Any()
                       ? Result.Failure<Task<TResponse>>(string.Join($", {Environment.NewLine}", failures))
                       : next();
        }
    }
}
using MediatR;

namespace Todo.Common.ServiceContracts
{
    public interface IValidatedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
    }
}

using FluentValidation;
using MediatR;
using MusicSite.Server.Commands;

namespace MusicSite.Server.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : IValidatedResponse
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var validation_results = await Task.WhenAll(
                    _validators
                    .Select(async x => await x.ValidateAsync(context, cancellationToken))
                );
            
            var failures = validation_results
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .ToList();

            if (failures.Any())
            {
                IValidatedResponse fail_response = ValidatedResponse<Unit>.FailedResponse(failures);
                return (TResponse)fail_response;
            }

            return await next();
        }
    }
}

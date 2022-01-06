using MediatR;
using FluentValidation;
using FluentValidation.Results;

namespace MusicSite.Server.Commands
{
    public class ValidatedResponse<T>
    {
        public T? Result { get; init; }

        public ICollection<ValidationFailure>? ValidationFailures { get; init; }

        public bool ValidationFailed()
        {
            return ValidationFailures is not null;
        }

        public static ValidatedResponse<T> FailedResponse(ICollection<ValidationFailure> failures)
        {
            return new ValidatedResponse<T>
            {
                ValidationFailures = failures
            };
        }

        public ValidatedResponse(T result)
        {
            Result = result;
        }

        public ValidatedResponse()
        {
        }
    }
}

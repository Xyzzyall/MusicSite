using MediatR;
using FluentValidation;
using FluentValidation.Results;

namespace MusicSite.Server.Commands
{
    public interface IValidatedResponse
    {
        bool ValidationFailed();
        string[] ValidationFailuresStringArray();
    }

    public class ValidatedResponse<T> : IValidatedResponse
    {
        public T? Result { get; init; }

        public ICollection<ValidationFailure>? ValidationFailures { get; init; }

        public bool ValidationFailed()
        {
            return ValidationFailures is not null;
        }

        public string[] ValidationFailuresStringArray()
        {
            if (!ValidationFailed())
            {
                throw new InvalidOperationException("Bad response or request was successful.");
            }
            return ValidationFailures.Select(x => x.ErrorMessage).ToArray();
        }

        public static ValidatedResponse<T> FailedResponse(ICollection<ValidationFailure> failures)
        {
            return new ValidatedResponse<T>
            {
                ValidationFailures = failures
            };
        }

        public static ValidatedResponse<T> TryCastResponse(IValidatedResponse response)
        {
            if (response is not ValidatedResponse<T> casted)
            {
                throw new InvalidCastException("Validated response type is invalid.");
            }
            return casted;
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

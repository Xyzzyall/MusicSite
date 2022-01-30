using MediatR;
using MusicSite.Shared.SharedModels;
using FluentValidation;
using MusicSite.Server.Validations.Releases;

namespace MusicSite.Server.Commands.Releases
{
    public class CreateReleaseCommand : IRequest<IValidatedResponse>
    {
        public ReleaseCreate Release { get; set; }

        public CreateReleaseCommand(ReleaseCreate release)
        {
            Release = release;
        }
    }

    public class CreateReleaseCommandValidator : AbstractValidator<CreateReleaseCommand>
    {
        public CreateReleaseCommandValidator()
        {
            //RuleFor(cmd => cmd.Release).SetValidator(new ReleaseSharedEditModeValidator());
        }
    }
}

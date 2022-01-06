using MediatR;
using MusicSite.Shared.SharedModels;
using FluentValidation;
using MusicSite.Server.Validations.Releases;

namespace MusicSite.Server.Commands.Releases
{
    public class CreateReleaseCommand : IRequest<ValidatedResponse<int>> 
    {
        public ReleaseSharedEditMode Release { get; set; }

        public CreateReleaseCommand(ReleaseSharedEditMode release)
        {
            Release = release;
        }
    }

    public class CreateReleaseCommandValidator : AbstractValidator<CreateReleaseCommand>
    {
        public CreateReleaseCommandValidator()
        {
            RuleFor(cmd => cmd.Release).SetValidator(new ReleaseSharedEditModeValidator());
        }
    }
}

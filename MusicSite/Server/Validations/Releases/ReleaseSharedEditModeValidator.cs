using FluentValidation;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Validations.Releases
{
    public class ReleaseSharedEditModeValidator : AbstractValidator<ReleaseSharedEditMode>
    {
        public ReleaseSharedEditModeValidator()
        {
            RuleFor(release => release.Codename).NotEmpty();
            RuleFor(release => release.Name).NotEmpty();
            RuleFor(release => release.Author).NotEmpty();
            RuleFor(release => release.Type).NotEmpty();
            RuleFor(release => release.Songs).NotEmpty()
                .When(release => release.IsReleased);
            RuleForEach(release => release.Songs).ChildRules(songs => 
                { 
                    songs.RuleFor(song => song.Name).NotEmpty();
                    songs.RuleFor(song => song.LengthSecs).NotEmpty();
                    /*songs.RuleFor(song => song.Lyrics).NotEmpty().When(
                        song => !(song.IsInstrumental)
                    );*/
                    songs.RuleFor(song => song.Lyrics).Empty().When(
                        song => song.IsInstrumental
                    );
                }
            );
        }
    }
}

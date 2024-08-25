using FluentValidation;

namespace Application.Features.Profiles.Commands.UpdateProfile;

public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileValidator()
    {
        RuleFor(x => x.DisplayName).NotEmpty();
    }
}

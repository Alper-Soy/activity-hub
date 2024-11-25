using Application.Features.Activities.Commons;
using FluentValidation;

namespace Application.Features.Activities.Commands.UpdateActivity;

public class UpdateActivityValidator : AbstractValidator<UpdateActivityCommand>
{
    public UpdateActivityValidator()
    {
        RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
    }
}

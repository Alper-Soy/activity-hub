using Application.Features.Activities.Commons;
using FluentValidation;

namespace Application.Features.Activities.Commands.CreateActivity;

public class CreateActivityValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityValidator()
    {
        RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
    }
}

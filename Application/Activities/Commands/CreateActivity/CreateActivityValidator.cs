using Application.Activities.Commons;
using FluentValidation;

namespace Application.Activities.Commands.CreateActivity;

public class CreateActivityValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityValidator()
    {
        RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
    }
}

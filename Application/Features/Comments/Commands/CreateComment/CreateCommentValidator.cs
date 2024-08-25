using FluentValidation;

namespace Application.Features.Comments.Commands.CreateComment;

public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        RuleFor(x => x.Body).NotEmpty();
    }
}

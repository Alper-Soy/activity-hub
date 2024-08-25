using Application.Core;
using Application.Features.Comments.Contracts;
using MediatR;

namespace Application.Features.Comments.Commands.CreateComment;

public class CreateCommentCommand : IRequest<Result<CommentDto>>
{
    public string Body { get; set; }
    public Guid ActivityId { get; set; }
}

using Application.Core;
using Application.Features.Comments.Contracts;
using MediatR;

namespace Application.Features.Comments.Queries.GetComments;

public class GetCommentsQuery : IRequest<Result<List<CommentDto>>>
{
    public Guid ActivityId { get; set; }
}

using Application.Core;
using Application.Features.Comments.Contracts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Comments.Queries.GetComments;

public class GetCommentsHandler(DataContext context, IMapper mapper)
    : IRequestHandler<GetCommentsQuery, Result<List<CommentDto>>>
{
    public async Task<Result<List<CommentDto>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await context.Comments
            .Where(x => x.Activity.Id == request.ActivityId)
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<CommentDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Result<List<CommentDto>>.Success(comments);
    }
}

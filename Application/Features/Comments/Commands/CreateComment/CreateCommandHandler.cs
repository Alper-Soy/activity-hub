using Application.Core;
using Application.Features.Comments.Contracts;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Comments.Commands.CreateComment;

public class CreateCommandHandler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
    : IRequestHandler<CreateCommentCommand, Result<CommentDto>>
{
    public async Task<Result<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync(request.ActivityId);

        if (activity == null) return null;

        var user = await context.Users.Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == userAccessor.GetUsername());

        var comment = new Comment
        {
            Author = user,
            Activity = activity,
            Body = request.Body
        };

        activity.Comments.Add(comment);

        var success = await context.SaveChangesAsync() > 0;

        return success
            ? Result<CommentDto>.Success(mapper.Map<CommentDto>(comment))
            : Result<CommentDto>.Failure("Failed to add comment");
    }
}

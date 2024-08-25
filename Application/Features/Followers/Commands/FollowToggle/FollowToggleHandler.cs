using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Followers.Commands.FollowToggle;

public class FollowToggleHandler(DataContext context, IUserAccessor userAccessor)
    : IRequestHandler<FollowToggleCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(FollowToggleCommand request, CancellationToken cancellationToken)
    {
        var observer = await context.Users.FirstOrDefaultAsync(x =>
            x.UserName == userAccessor.GetUsername());

        var target = await context.Users.FirstOrDefaultAsync(x =>
            x.UserName == request.TargetUsername);

        if (target == null) return null;

        var following = await context.UserFollowings.FindAsync(observer.Id, target.Id);

        if (following == null)
        {
            following = new UserFollowing
            {
                Observer = observer,
                Target = target
            };

            context.UserFollowings.Add(following);
        }
        else
        {
            context.UserFollowings.Remove(following);
        }

        var success = await context.SaveChangesAsync() > 0;

        return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to update following");
    }
}

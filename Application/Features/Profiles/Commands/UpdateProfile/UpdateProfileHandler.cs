using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Profiles.Commands.UpdateProfile;

public class UpdateProfileHandler(DataContext context, IUserAccessor userAccessor)
    : IRequestHandler<UpdateProfileCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x =>
            x.UserName == userAccessor.GetUsername());

        user.Bio = request.Bio ?? user.Bio;
        user.DisplayName = request.DisplayName ?? user.DisplayName;

        var success = await context.SaveChangesAsync() > 0;

        return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating profile");
    }
}

using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Photos.Commands.SetMainPhoto;

public class SetMainPhotoHandler(DataContext context, IUserAccessor userAccessor)
    : IRequestHandler<SetMainPhotoCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername());

        var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);

        if (photo == null) return null;

        var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

        if (currentMain != null) currentMain.IsMain = false;

        photo.IsMain = true;
        var success = await context.SaveChangesAsync() > 0;

        return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem setting main photo");
    }
}

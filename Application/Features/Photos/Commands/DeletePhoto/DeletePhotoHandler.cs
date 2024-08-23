using Application.Core;
using Application.Features.Photos.Commands.AddPhoto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Photos.Commands.DeletePhoto;

public class DeletePhotoHandler(DataContext context, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
    : IRequestHandler<DeletePhotoCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername());

        var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);

        if (photo == null) return null;

        if (photo.IsMain) return Result<Unit>.Failure("You cannot delete your main photo");

        var result = await photoAccessor.DeletePhoto(photo.Id);

        if (result == null) return Result<Unit>.Failure("Problem deleting photo");

        user.Photos.Remove(photo);

        var success = await context.SaveChangesAsync() > 0;

        return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem deleting photo");
    }
}

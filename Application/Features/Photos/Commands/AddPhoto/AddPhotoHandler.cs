using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Photos.Commands.AddPhoto;

public class AddPhotoHandler(DataContext context, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
    : IRequestHandler<AddPhotoCommand, Result<Photo>>
{
    public async Task<Result<Photo>> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername());

        if (user == null) return null;

        var photoUploadResult = await photoAccessor.AddPhoto(request.File);

        var photo = new Photo
        {
            Url = photoUploadResult.Url,
            Id = photoUploadResult.PublicId
        };

        if (!user.Photos.Any(x => x.IsMain)) photo.IsMain = true;

        user.Photos.Add(photo);

        var result = await context.SaveChangesAsync() > 0;

        return result ? Result<Photo>.Success(photo) : Result<Photo>.Failure("Problem adding photo");
    }
}

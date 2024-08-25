using Application.Core;
using MediatR;

namespace Application.Features.Photos.Commands.SetMainPhoto;

public class SetMainPhotoCommand : IRequest<Result<Unit>>
{
    public string Id { get; set; }
}

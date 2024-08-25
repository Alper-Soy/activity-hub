using Application.Core;
using MediatR;

namespace Application.Features.Photos.Commands.DeletePhoto;

public class DeletePhotoCommand : IRequest<Result<Unit>>
{
    public string Id { get; set; }
}

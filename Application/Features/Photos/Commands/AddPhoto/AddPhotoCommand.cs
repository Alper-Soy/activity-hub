using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Photos.Commands.AddPhoto;

public class AddPhotoCommand : IRequest<Result<Photo>>
{
    public IFormFile File { get; set; }
}

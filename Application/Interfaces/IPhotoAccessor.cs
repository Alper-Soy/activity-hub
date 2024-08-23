using Application.Features.Photos.Commands.AddPhoto;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IPhotoAccessor
{
    Task<PhotoUploadResult> AddPhoto(IFormFile file);
    Task<string> DeletePhoto(string publicId);
}

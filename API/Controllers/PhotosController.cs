using Application.Features.Photos.Commands.AddPhoto;
using Application.Features.Photos.Commands.DeletePhoto;
using Application.Features.Photos.Commands.SetMainPhoto;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PhotosController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AddPhotoCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return HandleResult(await Mediator.Send(new DeletePhotoCommand { Id = id }));
    }

    [HttpPost("{id}/setMain")]
    public async Task<IActionResult> SetMain(string id)
    {
        return HandleResult(await Mediator.Send(new SetMainPhotoCommand { Id = id }));
    }
}

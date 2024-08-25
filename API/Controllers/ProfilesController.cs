using Application.Features.Profiles.Commands.UpdateProfile;
using Application.Features.Profiles.Queries.GetProfile;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfilesController : BaseApiController
{
    [HttpGet("{username}")]
    public async Task<IActionResult> GetProfile(string username)
    {
        return HandleResult(await Mediator.Send(new GetProfileQuery { Username = username }));
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(UpdateProfileCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }
}



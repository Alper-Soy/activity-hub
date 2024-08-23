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
}

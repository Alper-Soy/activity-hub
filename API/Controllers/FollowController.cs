using Application.Features.Followers.Commands.FollowToggle;
using Application.Features.Followers.Queries.Follow;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class FollowController : BaseApiController
{
    [HttpPost("{username}")]
    public async Task<IActionResult> Follow(string username)
    {
        return HandleResult(await Mediator.Send(new FollowToggleCommand
            { TargetUsername = username }));
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetFollowings(string username, string predicate)
    {
        return HandleResult(await Mediator.Send(new GetFollowQuery { Username = username, Predicate = predicate }));
    }
}

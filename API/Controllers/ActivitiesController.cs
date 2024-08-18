using Application.Activities.Commands.CreateActivity;
using Application.Activities.Commands.DeleteActivity;
using Application.Activities.Commands.UpdateActivity;
using Application.Activities.Queries.GetActivities;
using Application.Activities.Queries.GetActivity;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetActivities()
    {
        return HandleResult(await Mediator.Send(new GetActivitiesQuery()));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetActivity(Guid id)
    {
        return HandleResult(await Mediator.Send(new GetActivityQuery { Id = id })
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity(Activity activity)
    {
        return HandleResult(await Mediator.Send(new CreateActivityCommand { Activity = activity }));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateActivity(Guid id, Activity activity)
    {
        activity.Id = id;
        return HandleResult(await Mediator.Send(new UpdateActivityCommand { Activity = activity }));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteActivityCommand { Id = id }));
    }
}

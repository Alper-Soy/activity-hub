using Application.Core;
using Application.Features.Activities.Commands.CreateActivity;
using Application.Features.Activities.Commands.DeleteActivity;
using Application.Features.Activities.Commands.UpdateActivity;
using Application.Features.Activities.Contracts;
using Application.Features.Activities.Queries.GetActivities;
using Application.Features.Activities.Queries.GetActivity;
using Application.Features.Attendance.Commands.UpdateAttendance;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetActivities([FromQuery] ActivityParams param)
    {
        return HandlePagedResult(await Mediator.Send(new GetActivitiesQuery { Params = param }));
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

    [Authorize(Policy = "IsActivityHost")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateActivity(Guid id, Activity activity)
    {
        activity.Id = id;
        return HandleResult(await Mediator.Send(new UpdateActivityCommand { Activity = activity }));
    }

    [Authorize(Policy = "IsActivityHost")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteActivityCommand { Id = id }));
    }

    [HttpPost("{id:guid}/attend")]
    public async Task<IActionResult> Attend(Guid id)
    {
        return HandleResult(await Mediator.Send(new UpdateAttendanceCommand { Id = id }));
    }
}

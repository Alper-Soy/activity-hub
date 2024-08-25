using Application.Features.Comments.Commands.CreateComment;
using Application.Features.Comments.Queries.GetComments;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR;

public class ChatHub(IMediator mediator) : Hub
{
    public async Task SendComment(CreateCommentCommand command)
    {
        var comment = await mediator.Send(command);

        await Clients.Group(command.ActivityId.ToString()).SendAsync("ReceiveComment", comment.Value);
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var activityId = httpContext!.Request.Query["activityId"];
        await Groups.AddToGroupAsync(Context.ConnectionId, activityId);
        var result = await mediator.Send(new GetCommentsQuery { ActivityId = Guid.Parse(activityId) });
        await Clients.Caller.SendAsync("LoadComments", result.Value);
    }
}

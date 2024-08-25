using Application.Core;
using MediatR;

namespace Application.Features.Followers.Commands.FollowToggle;

public class FollowToggleCommand : IRequest<Result<Unit>>
{
    public string TargetUsername { get; set; }
}

using Application.Core;
using Application.Features.Profiles.Contracts;
using MediatR;

namespace Application.Features.Followers.Queries.Follow;

public class GetFollowQuery : IRequest<Result<List<ProfileDto>>>
{
    public string Predicate { get; set; }
    public string Username { get; set; }
}

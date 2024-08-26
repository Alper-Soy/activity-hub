using Application.Core;
using Application.Features.Profiles.Contracts;
using MediatR;

namespace Application.Features.Profiles.Queries.GetActivities;

public class GetActivitiesQuery : IRequest<Result<List<UserActivityDto>>>
{
    public string Username { get; set; }
    public string Predicate { get; set; }
}

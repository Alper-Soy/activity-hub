using Application.Core;
using Application.Features.Profiles.Contracts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Profiles.Queries.GetActivities;

public class GetActivitiesHandler(DataContext context, IMapper mapper) : IRequestHandler<GetActivitiesQuery,
    Result<List<UserActivityDto>>>
{
    public async Task<Result<List<UserActivityDto>>> Handle(GetActivitiesQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.ActivityAttendees
            .Where(u => u.AppUser.UserName == request.Username)
            .OrderBy(a => a.Activity.Date)
            .ProjectTo<UserActivityDto>(mapper.ConfigurationProvider)
            .AsQueryable();
        query = request.Predicate switch
        {
            "past" => query.Where(a => a.Date <= DateTime.Now),
            "hosting" => query.Where(a => a.HostUsername == request.Username),
            _ => query.Where(a => a.Date >= DateTime.Now)
        };
        var activities = await query.ToListAsync();
        return Result<List<UserActivityDto>>.Success(activities);
    }
}

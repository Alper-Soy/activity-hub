using Application.Core;
using Application.Features.Activities.Contracts;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Activities.Queries.GetActivities;

public class GetActivitiesHandler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
    : IRequestHandler<GetActivitiesQuery, Result<List<ActivityDto>>>
{
    public async Task<Result<List<ActivityDto>>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        var activities = await context.Activities.ProjectTo<ActivityDto>(mapper.ConfigurationProvider,
                new { currentUsername = userAccessor.GetUsername() })
            .ToListAsync(cancellationToken);

        return Result<List<ActivityDto>>.Success(activities);
    }
}

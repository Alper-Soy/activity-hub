using Application.Core;
using Application.Features.Activities.Contracts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Activities.Queries.GetActivities;

public class GetActivitiesHandler(DataContext context, IMapper mapper)
    : IRequestHandler<GetActivitiesQuery, Result<List<ActivityDto>>>
{
    public async Task<Result<List<ActivityDto>>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        var activities = await context.Activities.ProjectTo<ActivityDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return Result<List<ActivityDto>>.Success(activities);
    }
}

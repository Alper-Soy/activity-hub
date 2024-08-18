using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries.GetActivities;

public class GetActivitiesHandler(DataContext context) : IRequestHandler<GetActivitiesQuery, Result<List<Activity>>>
{
    public async Task<Result<List<Activity>>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        return Result<List<Activity>>.Success(await context.Activities.ToListAsync());
    }
}

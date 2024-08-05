using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries.GetActivities;

public class GetActivitiesHandler(DataContext context) : IRequestHandler<GetActivitiesQuery, List<Activity>>
{
    public async Task<List<Activity>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        return await context.Activities.ToListAsync();
    }
}

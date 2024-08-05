using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Activities.Queries.GetActivity;

public class GetActivityHandler(DataContext context) : IRequestHandler<GetActivityQuery, Activity>
{
    public async Task<Activity> Handle(GetActivityQuery request, CancellationToken cancellationToken)
    {
        return await context.Activities.FindAsync(request.Id);
    }
}

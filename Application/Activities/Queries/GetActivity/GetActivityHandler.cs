using Application.Core;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Activities.Queries.GetActivity;

public class GetActivityHandler(DataContext context) : IRequestHandler<GetActivityQuery, Result<Activity>>
{
    public async Task<Result<Activity>> Handle(GetActivityQuery request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync(request.Id);

        return Result<Activity>.Success(activity);
    }
}

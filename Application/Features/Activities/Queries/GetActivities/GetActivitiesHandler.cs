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
    : IRequestHandler<GetActivitiesQuery, Result<PagedList<ActivityDto>>>
{
    public async Task<Result<PagedList<ActivityDto>>> Handle(GetActivitiesQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Activities.Where(d => d.Date >= request.Params.StartDate).OrderBy(d => d.Date)
            .ProjectTo<ActivityDto>(mapper.ConfigurationProvider,
                new { currentUsername = userAccessor.GetUsername() })
            .AsQueryable();

        if (request.Params.IsGoing && !request.Params.IsHost)
        {
            query = query.Where(x => x.Attendees.Any(a => a.Username == userAccessor.GetUsername()));
        }

        if (request.Params.IsHost && !request.Params.IsGoing)
        {
            query = query.Where(x => x.HostUsername == userAccessor.GetUsername());
        }

        return Result<PagedList<ActivityDto>>.Success(
            await PagedList<ActivityDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize));
    }
}

using Application.Core;
using Application.Features.Profiles.Contracts;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Followers.Queries.Follow;

public class GetFollowHandler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
    : IRequestHandler<GetFollowQuery, Result<List<ProfileDto>>>
{
    public async Task<Result<List<ProfileDto>>> Handle(GetFollowQuery request, CancellationToken cancellationToken)
    {
        var profiles = new List<ProfileDto>();

        switch (request.Predicate)
        {
            case "followers":
                profiles = await context.UserFollowings.Where(x => x.Target.UserName == request.Username)
                    .Select(u => u.Observer)
                    .ProjectTo<ProfileDto>(mapper.ConfigurationProvider,
                        new { currentUsername = userAccessor.GetUsername() })
                    .ToListAsync();
                break;
            case "following":
                profiles = await context.UserFollowings.Where(x => x.Observer.UserName == request.Username)
                    .Select(u => u.Target)
                    .ProjectTo<ProfileDto>(mapper.ConfigurationProvider,
                        new { currentUsername = userAccessor.GetUsername() })
                    .ToListAsync();
                break;
        }

        return Result<List<ProfileDto>>.Success(profiles);
    }
}

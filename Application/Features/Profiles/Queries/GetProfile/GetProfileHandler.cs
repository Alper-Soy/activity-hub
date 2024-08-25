using Application.Core;
using Application.Features.Profiles.Contracts;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Profiles.Queries.GetProfile;

public class GetProfileHandler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
    : IRequestHandler<GetProfileQuery, Result<ProfileDto>>
{
    public async Task<Result<ProfileDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .ProjectTo<ProfileDto>(mapper.ConfigurationProvider, new { currentUsername = userAccessor.GetUsername() })
            .SingleOrDefaultAsync(x => x.Username == request.Username);

        return Result<ProfileDto>.Success(user);
    }
}

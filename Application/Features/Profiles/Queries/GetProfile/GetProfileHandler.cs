using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Profile = Application.Features.Profiles.Contracts.Profile;

namespace Application.Features.Profiles.Queries.GetProfile;

public class GetProfileHandler(DataContext context, IMapper mapper) : IRequestHandler<GetProfileQuery, Result<Profile>>
{
    public async Task<Result<Profile>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .ProjectTo<Profile>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Username == request.Username);

        return Result<Profile>.Success(user);
    }
}

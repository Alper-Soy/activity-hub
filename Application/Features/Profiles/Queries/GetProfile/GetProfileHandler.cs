using Application.Core;
using Application.Features.Profiles.Contracts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Profiles.Queries.GetProfile;

public class GetProfileHandler(DataContext context, IMapper mapper)
    : IRequestHandler<GetProfileQuery, Result<ProfileDto>>
{
    public async Task<Result<ProfileDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .ProjectTo<ProfileDto>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Username == request.Username);

        return Result<ProfileDto>.Success(user);
    }
}

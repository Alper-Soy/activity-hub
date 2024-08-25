using Application.Core;
using Application.Features.Profiles.Contracts;
using MediatR;

namespace Application.Features.Profiles.Queries.GetProfile;

public class GetProfileQuery : IRequest<Result<ProfileDto>>
{
    public string Username { get; set; }
}

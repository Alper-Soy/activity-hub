using Application.Core;
using Application.Features.Profiles.Contracts;
using MediatR;

namespace Application.Features.Profiles.Queries.GetProfile;

public class GetProfileQuery : IRequest<Result<Profile>>
{
    public string Username { get; set; }
}

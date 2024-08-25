using Application.Core;
using MediatR;

namespace Application.Features.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<Result<Unit>>
{
    public string DisplayName { get; set; }
    public string Bio { get; set; }
}

using Domain.Entities;

namespace Application.Features.Profiles.Contracts;

public class Profile
{
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string Bio { get; set; }
    public string Image { get; set; }
    public ICollection<Photo> Photos { get; set; }
}

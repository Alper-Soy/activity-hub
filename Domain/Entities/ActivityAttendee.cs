namespace Domain.Entities;

public class ActivityAttendee
{
    public string UserId { get; set; }
    public AppUser AppUser { get; set; }
    public Guid ActivityId { get; set; }
    public Activity Activity { get; set; }
    public bool IsHost { get; set; }
}

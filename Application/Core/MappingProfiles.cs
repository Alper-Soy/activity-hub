using Application.Features.Activities.Contracts;
using Application.Features.Attendance.Contracts;
using Application.Features.Comments.Contracts;
using AutoMapper;
using Domain.Entities;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Activity, Activity>();

        CreateMap<Activity, ActivityDto>()
            .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.Attendees
                .FirstOrDefault(x => x.IsHost).User.UserName));

        CreateMap<ActivityAttendee, AttendeeDto>()
            .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.User.DisplayName))
            .ForMember(d => d.Username, o => o.MapFrom(s => s.User.UserName))
            .ForMember(d => d.Bio, o => o.MapFrom(s => s.User.Bio))
            .ForMember(d => d.Image, o => o.MapFrom(s => s.User.Photos.FirstOrDefault(x => x.IsMain).Url));

        CreateMap<User, Features.Profiles.Contracts.ProfileDto>()
            .ForMember(d => d.Image, s => s.MapFrom(o => o.Photos.FirstOrDefault(x => x.IsMain).Url));

        CreateMap<Comment, CommentDto>()
            .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
            .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
            .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
    }
}

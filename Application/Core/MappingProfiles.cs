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
        string currentUsername = null;
        CreateMap<Activity, Activity>();

        CreateMap<Activity, ActivityDto>()
            .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.Attendees
                .FirstOrDefault(x => x.IsHost).AppUser.UserName));

        CreateMap<ActivityAttendee, AttendeeDto>()
            .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
            .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
            .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio))
            .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.AppUser.Followers.Count))
            .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.AppUser.Followings.Count))
            .ForMember(d => d.Following,
                o => o.MapFrom(s => s.AppUser.Followers.Any(x => x.Observer.UserName == currentUsername)));

        CreateMap<AppUser, Features.Profiles.Contracts.ProfileDto>()
            .ForMember(d => d.Image, s => s.MapFrom(o => o.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.Followers.Count))
            .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.Followings.Count))
            .ForMember(d => d.Following,
                o => o.MapFrom(s => s.Followers.Any(x => x.Observer.UserName == currentUsername)));

        CreateMap<Comment, CommentDto>()
            .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
            .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
            .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
    }
}

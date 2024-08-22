using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Attendance.Commands.UpdateAttendance;

public class UpdateAttendanceHandler(DataContext context, IUserAccessor userAccessor)
    : IRequestHandler<UpdateAttendanceCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername(),
            cancellationToken);

        if (user == null) return null;

        var activity = await context.Activities.Include(a => a.Attendees).ThenInclude(u => u.User)
            .SingleOrDefaultAsync(x => x.Id == request.Id);

        if (activity == null) return null;

        var hostUsername = activity.Attendees.FirstOrDefault(x => x.IsHost)?.User?.UserName;

        var attendance = activity.Attendees.FirstOrDefault(x => x.User.UserName == user.UserName);

        if (attendance != null && hostUsername == user.UserName) activity.IsCancelled = !activity.IsCancelled;

        if (attendance != null && hostUsername != user.UserName) activity.Attendees.Remove(attendance);

        if (attendance == null)
        {
            attendance = new ActivityAttendee
            {
                User = user,
                Activity = activity,
                IsHost = false
            };

            activity.Attendees.Add(attendance);
        }

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updated attendance");
    }
}

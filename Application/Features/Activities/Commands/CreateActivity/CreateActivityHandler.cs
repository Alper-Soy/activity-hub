using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Activities.Commands.CreateActivity;

public class CreateActivityHandler(DataContext context, IUserAccessor userAccessor)
    : IRequestHandler<CreateActivityCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername(),
            cancellationToken);

        var attendee = new ActivityAttendee
        {
            AppUser = user,
            Activity = request.Activity,
            IsHost = true
        };

        request.Activity.Attendees.Add(attendee);

        context.Activities.Add(request.Activity);

        var result = await context.SaveChangesAsync() > 0;

        return !result ? Result<Unit>.Failure("Failed to create activity") : Result<Unit>.Success(Unit.Value);
    }
}

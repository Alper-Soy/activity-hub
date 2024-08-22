using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Features.Activities.Commands.UpdateActivity;

public class UpdateActivityHandler(DataContext context, IMapper mapper)
    : IRequestHandler<UpdateActivityCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync(request.Activity.Id);

        if (activity == null) return null;

        mapper.Map(request.Activity, activity);

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return !result ? Result<Unit>.Failure("Failed to update activity") : Result<Unit>.Success(Unit.Value);
    }
}

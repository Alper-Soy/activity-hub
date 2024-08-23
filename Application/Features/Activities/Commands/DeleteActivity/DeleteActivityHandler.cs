using Application.Core;
using MediatR;
using Persistence;

namespace Application.Features.Activities.Commands.DeleteActivity;

public class DeleteActivityHandler(DataContext context) : IRequestHandler<DeleteActivityCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync(request.Id);

        if (activity == null) return null;

        context.Remove(activity);

        var result = await context.SaveChangesAsync() > 0;

        if (!result) Result<Unit>.Failure("Failed to delete the activity");

        return Result<Unit>.Success(Unit.Value);
    }
}

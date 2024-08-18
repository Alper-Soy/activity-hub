using Application.Core;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.CreateActivity;

public class CreateActivityHandler(DataContext context) : IRequestHandler<CreateActivityCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        context.Activities.Add(request.Activity);

        var result = await context.SaveChangesAsync() > 0;

        return !result ? Result<Unit>.Failure("Failed to create activity") : Result<Unit>.Success(Unit.Value);
    }
}

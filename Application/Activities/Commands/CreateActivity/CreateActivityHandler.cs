using MediatR;
using Persistence;

namespace Application.Activities.Commands.CreateActivity;

public class CreateActivityHandler(DataContext context) : IRequestHandler<CreateActivityCommand>
{
    public async Task Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        context.Activities.Add(request.Activity);
        await context.SaveChangesAsync();
    }
}

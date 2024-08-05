using MediatR;
using Persistence;

namespace Application.Activities.Commands.DeleteActivity;

public class DeleteActivityHandler(DataContext context) : IRequestHandler<DeleteActivityCommand>
{
    public async Task Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync(request.Id);

        context.Remove(activity);

        await context.SaveChangesAsync();
    }
}

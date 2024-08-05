using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.UpdateActivity;

public class UpdateActivityHandler(DataContext context, IMapper mapper) : IRequestHandler<UpdateActivityCommand>
{
    public async Task Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync(request.Activity.Id);

        mapper.Map(request.Activity, activity);

        await context.SaveChangesAsync();
    }
}

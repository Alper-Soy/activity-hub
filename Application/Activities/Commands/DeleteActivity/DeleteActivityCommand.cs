using MediatR;

namespace Application.Activities.Commands.DeleteActivity;

public class DeleteActivityCommand : IRequest
{
    public Guid Id { get; set; }
}

using Application.Core;
using MediatR;

namespace Application.Features.Activities.Commands.DeleteActivity;

public class DeleteActivityCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }
}

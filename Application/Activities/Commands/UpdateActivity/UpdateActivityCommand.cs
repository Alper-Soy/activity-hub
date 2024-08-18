using Application.Core;
using Domain.Entities;
using MediatR;

namespace Application.Activities.Commands.UpdateActivity;

public class UpdateActivityCommand : IRequest<Result<Unit>>
{
    public Activity Activity { get; set; }
}

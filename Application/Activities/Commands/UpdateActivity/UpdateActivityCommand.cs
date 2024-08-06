using Domain.Entities;
using MediatR;

namespace Application.Activities.Commands.UpdateActivity;

public class UpdateActivityCommand : IRequest
{
    public Activity Activity { get; set; }
}

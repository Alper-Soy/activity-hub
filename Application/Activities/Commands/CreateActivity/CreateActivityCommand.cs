using Domain.Entities;
using MediatR;

namespace Application.Activities.Commands.CreateActivity;

public class CreateActivityCommand : IRequest
{
    public Activity Activity { get; set; }
}

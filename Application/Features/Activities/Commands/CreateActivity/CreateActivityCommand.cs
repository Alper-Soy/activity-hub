using Application.Core;
using Domain.Entities;
using MediatR;

namespace Application.Features.Activities.Commands.CreateActivity;

public class CreateActivityCommand : IRequest<Result<Unit>>
{
    public Activity Activity { get; set; }
}

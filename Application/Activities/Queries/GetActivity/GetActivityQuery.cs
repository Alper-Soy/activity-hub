using Application.Core;
using Domain.Entities;
using MediatR;

namespace Application.Activities.Queries.GetActivity;

public class GetActivityQuery : IRequest<Result<Activity>>
{
    public Guid Id { get; set; }
}

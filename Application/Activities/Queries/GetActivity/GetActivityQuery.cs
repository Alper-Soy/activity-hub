using Domain.Entities;
using MediatR;

namespace Application.Activities.Queries.GetActivity;

public class GetActivityQuery : IRequest<Activity>
{
    public Guid Id { get; set; }
}

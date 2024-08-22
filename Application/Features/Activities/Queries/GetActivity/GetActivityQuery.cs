using Application.Core;
using Application.Features.Activities.Contracts;
using MediatR;

namespace Application.Features.Activities.Queries.GetActivity;

public class GetActivityQuery : IRequest<Result<ActivityDto>>
{
    public Guid Id { get; set; }
}

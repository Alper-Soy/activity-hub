using Application.Core;
using Application.Features.Activities.Contracts;
using MediatR;

namespace Application.Features.Activities.Queries.GetActivities;

public class GetActivitiesQuery : IRequest<Result<List<ActivityDto>>>;

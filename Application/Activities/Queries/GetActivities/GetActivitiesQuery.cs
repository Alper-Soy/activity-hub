using Application.Core;
using Domain.Entities;
using MediatR;

namespace Application.Activities.Queries.GetActivities;

public class GetActivitiesQuery : IRequest<Result<List<Activity>>>;

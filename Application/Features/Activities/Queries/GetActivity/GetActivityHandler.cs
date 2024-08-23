using Application.Core;
using Application.Features.Activities.Contracts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Activities.Queries.GetActivity;

public class GetActivityHandler(DataContext context, IMapper mapper)
    : IRequestHandler<GetActivityQuery, Result<ActivityDto>>
{
    public async Task<Result<ActivityDto>> Handle(GetActivityQuery request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.ProjectTo<ActivityDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        return Result<ActivityDto>.Success(activity);
    }
}

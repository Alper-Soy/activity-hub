using Application.Core;
using MediatR;

namespace Application.Features.Attendance.Commands.UpdateAttendance;

public class UpdateAttendanceCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }
}

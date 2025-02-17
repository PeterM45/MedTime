namespace MedTime.Application.Features.Appointments.Commands.ScheduleAppointment;

using MediatR;

public record ScheduleAppointmentCommand : IRequest<Guid>
{
    public Guid PatientId { get; init; }
    public Guid DoctorId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public string? Notes { get; init; }
}

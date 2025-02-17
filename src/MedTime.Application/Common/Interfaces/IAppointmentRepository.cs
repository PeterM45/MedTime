namespace MedTime.Application.Common.Interfaces;

using MedTime.Domain.Entities;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken);
    Task<Appointment> AddAsync(Appointment appointment, CancellationToken cancellationToken);
    Task<bool> HasConflictingAppointmentAsync(Guid doctorId, DateTime startTime, DateTime endTime, CancellationToken cancellationToken);
}

namespace MedTime.Application.Common.Interfaces;

using MedTime.Domain.Entities;

public interface IDoctorRepository
{
    Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Doctor> AddAsync(Doctor doctor, CancellationToken cancellationToken);
    Task UpdateAsync(Doctor doctor, CancellationToken cancellationToken);
    Task<IEnumerable<Doctor>> GetActiveAsync(CancellationToken cancellationToken);
}

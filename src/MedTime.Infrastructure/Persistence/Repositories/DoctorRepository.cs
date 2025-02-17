namespace MedTime.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using MedTime.Application.Common.Interfaces;
using MedTime.Domain.Entities;

public class DoctorRepository : IDoctorRepository
{
	private readonly ApplicationDbContext _context;

	public DoctorRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await _context.Doctors
			.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
	}

	public async Task<Doctor> AddAsync(Doctor doctor, CancellationToken cancellationToken)
	{
		_context.Doctors.Add(doctor);
		await _context.SaveChangesAsync(cancellationToken);
		return doctor;
	}

	public async Task UpdateAsync(Doctor doctor, CancellationToken cancellationToken)
	{
		_context.Entry(doctor).State = EntityState.Modified;
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<IEnumerable<Doctor>> GetActiveAsync(CancellationToken cancellationToken)
	{
		return await _context.Doctors
			.Where(d => d.IsActive)
			.ToListAsync(cancellationToken);
	}
}
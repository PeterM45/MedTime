namespace MedTime.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using MedTime.Application.Common.Interfaces;
using MedTime.Domain.Entities;

public class PatientRepository : IPatientRepository
{
	private readonly ApplicationDbContext _context;

	public PatientRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await _context.Patients
			.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
	}

	public async Task<Patient> AddAsync(Patient patient, CancellationToken cancellationToken)
	{
		_context.Patients.Add(patient);
		await _context.SaveChangesAsync(cancellationToken);
		return patient;
	}

	public async Task UpdateAsync(Patient patient, CancellationToken cancellationToken)
	{
		_context.Entry(patient).State = EntityState.Modified;
		await _context.SaveChangesAsync(cancellationToken);
	}
}
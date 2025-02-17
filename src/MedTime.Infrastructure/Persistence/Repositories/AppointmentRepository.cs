namespace MedTime.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using MedTime.Application.Common.Interfaces;
using MedTime.Domain.Entities;

public class AppointmentRepository : IAppointmentRepository
{
	private readonly ApplicationDbContext _context;

	public AppointmentRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Appointment> AddAsync(Appointment appointment, CancellationToken cancellationToken)
	{
		_context.Appointments.Add(appointment);
		await _context.SaveChangesAsync(cancellationToken);
		return appointment;
	}

	public async Task<bool> HasConflictingAppointmentAsync(Guid doctorId, DateTime startTime, DateTime endTime,
		CancellationToken cancellationToken)
	{
		return await _context.Appointments
			.AnyAsync(a =>
				a.DoctorId == doctorId &&
				a.Status == AppointmentStatus.Scheduled &&
				((startTime >= a.StartTime && startTime < a.EndTime) ||
				 (endTime > a.StartTime && endTime <= a.EndTime) ||
				 (startTime <= a.StartTime && endTime >= a.EndTime)),
				cancellationToken);
	}

	public async Task<Appointment?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken)
	{
		return await _context.Appointments
			.Include(a => a.Patient)
			.Include(a => a.Doctor)
			.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
	}
}
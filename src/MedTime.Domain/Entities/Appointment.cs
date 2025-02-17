namespace MedTime.Domain.Entities;

using MedTime.Domain.Common;
using MedTime.Domain.Exceptions;

public class Appointment : Entity
{
	public Guid PatientId { get; private set; }
	public Guid DoctorId { get; private set; }
	public DateTime StartTime { get; private set; }
	public DateTime EndTime { get; private set; }
	public AppointmentStatus Status { get; private set; }
	public string? Notes { get; private set; }

	public Patient Patient { get; private set; } = null!; // Navigation property
	public Doctor Doctor { get; private set; } = null!;   // Navigation property

	private Appointment() { } // Required by EF Core

	public Appointment(Patient patient, Doctor doctor, DateTime startTime, DateTime endTime, string? notes = null)
		: base()
	{
		ValidateAppointment(patient, doctor, startTime, endTime);

		PatientId = patient.Id;
		DoctorId = doctor.Id;
		StartTime = startTime;
		EndTime = endTime;
		Status = AppointmentStatus.Scheduled;
		Notes = notes;

		Patient = patient;
		Doctor = doctor;
	}

	public void Cancel(string? cancellationNotes = null)
	{
		if (Status != AppointmentStatus.Scheduled)
			throw new DomainException("Only scheduled appointments can be cancelled");

		if (StartTime <= DateTime.UtcNow)
			throw new DomainException("Cannot cancel past or ongoing appointments");

		Status = AppointmentStatus.Cancelled;
		Notes = cancellationNotes ?? Notes;
	}

	public void Complete(string? completionNotes = null)
	{
		if (Status != AppointmentStatus.Scheduled)
			throw new DomainException("Only scheduled appointments can be completed");

		if (StartTime > DateTime.UtcNow)
			throw new DomainException("Cannot complete future appointments");

		Status = AppointmentStatus.Completed;
		Notes = completionNotes ?? Notes;
	}

	private static void ValidateAppointment(Patient patient, Doctor doctor, DateTime startTime, DateTime endTime)
	{
		if (patient == null)
			throw new DomainException("Patient is required");

		if (doctor == null)
			throw new DomainException("Doctor is required");

		if (!doctor.IsActive)
			throw new DomainException("Cannot schedule appointment with inactive doctor");

		if (startTime >= endTime)
			throw new DomainException("End time must be after start time");

		if (endTime - startTime > TimeSpan.FromHours(4))
			throw new DomainException("Appointment duration cannot exceed 4 hours");

		if (startTime <= DateTime.UtcNow)
			throw new DomainException("Cannot schedule appointments in the past");
	}
}

public enum AppointmentStatus
{
	Scheduled,
	Completed,
	Cancelled
}
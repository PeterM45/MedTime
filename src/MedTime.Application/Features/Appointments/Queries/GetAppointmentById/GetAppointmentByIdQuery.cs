namespace MedTime.Application.Features.Appointments.Queries.GetAppointmentById;

using MediatR;
using MedTime.Application.Common.Interfaces;
using MedTime.Domain.Exceptions;

public record GetAppointmentByIdQuery(Guid Id) : IRequest<AppointmentDto>;

public record AppointmentDto(
	Guid Id,
	string PatientName,
	string DoctorName,
	DateTime StartTime,
	DateTime EndTime,
	string Status,
	string? Notes);

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
{
	private readonly IAppointmentRepository _appointmentRepository;

	public GetAppointmentByIdQueryHandler(IAppointmentRepository appointmentRepository)
	{
		_appointmentRepository = appointmentRepository;
	}

	public async Task<AppointmentDto> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
	{
		var appointment = await _appointmentRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken)
			?? throw new DomainException($"Appointment with ID {request.Id} not found");

		return new AppointmentDto(
			appointment.Id,
			appointment.Patient.PersonInfo.FullName,
			appointment.Doctor.PersonInfo.FullName,
			appointment.StartTime,
			appointment.EndTime,
			appointment.Status.ToString(),
			appointment.Notes);
	}
}
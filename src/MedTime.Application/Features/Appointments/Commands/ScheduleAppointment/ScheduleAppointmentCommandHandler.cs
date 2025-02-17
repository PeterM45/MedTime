namespace MedTime.Application.Features.Appointments.Commands.ScheduleAppointment;

using MediatR;
using MedTime.Application.Common.Interfaces;
using MedTime.Domain.Entities;
using MedTime.Domain.Exceptions;

public class ScheduleAppointmentCommandHandler : IRequestHandler<ScheduleAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;

    public ScheduleAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
    }

    public async Task<Guid> Handle(ScheduleAppointmentCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken)
            ?? throw new DomainException($"Patient with ID {request.PatientId} not found");

        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId, cancellationToken)
            ?? throw new DomainException($"Doctor with ID {request.DoctorId} not found");

        // Check for conflicting appointments
        if (await _appointmentRepository.HasConflictingAppointmentAsync(
            request.DoctorId, request.StartTime, request.EndTime, cancellationToken))
        {
            throw new DomainException("The selected time slot conflicts with an existing appointment");
        }

        var appointment = new Appointment(
            patient,
            doctor,
            request.StartTime,
            request.EndTime,
            request.Notes);

        await _appointmentRepository.AddAsync(appointment, cancellationToken);

        return appointment.Id;
    }
}

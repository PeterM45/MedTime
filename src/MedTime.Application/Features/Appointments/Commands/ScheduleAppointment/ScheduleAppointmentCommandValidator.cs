namespace MedTime.Application.Features.Appointments.Commands.ScheduleAppointment;

using FluentValidation;

public class ScheduleAppointmentCommandValidator : AbstractValidator<ScheduleAppointmentCommand>
{
    public ScheduleAppointmentCommandValidator()
    {
        RuleFor(v => v.PatientId)
            .NotEmpty()
            .WithMessage("Patient ID is required");

        RuleFor(v => v.DoctorId)
            .NotEmpty()
            .WithMessage("Doctor ID is required");

        RuleFor(v => v.StartTime)
            .NotEmpty()
            .WithMessage("Start time is required")
            .Must(startTime => startTime > DateTime.UtcNow)
            .WithMessage("Start time must be in the future");

        RuleFor(v => v.EndTime)
            .NotEmpty()
            .WithMessage("End time is required")
            .Must((command, endTime) => endTime > command.StartTime)
            .WithMessage("End time must be after start time");

        RuleFor(v => v.Notes)
            .MaximumLength(500)
            .When(v => v.Notes != null)
            .WithMessage("Notes cannot exceed 500 characters");
    }
}

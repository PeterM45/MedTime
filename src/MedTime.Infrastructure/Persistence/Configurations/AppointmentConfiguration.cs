namespace MedTime.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedTime.Domain.Entities;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
	public void Configure(EntityTypeBuilder<Appointment> builder)
	{
		builder.HasKey(a => a.Id);

		builder.Property(a => a.StartTime)
			.IsRequired();

		builder.Property(a => a.EndTime)
			.IsRequired();

		builder.Property(a => a.Status)
			.IsRequired();

		builder.Property(a => a.Notes)
			.HasMaxLength(1000);

		builder.HasOne(a => a.Patient)
			.WithMany()
			.HasForeignKey(a => a.PatientId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(a => a.Doctor)
			.WithMany()
			.HasForeignKey(a => a.DoctorId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
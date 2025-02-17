namespace MedTime.Infrastructure.Persistence.Configurations;

using MedTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(d => d.Id);

        builder.OwnsOne(d => d.PersonInfo, info =>
        {
            info.Property(pi => pi.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            info.Property(pi => pi.LastName)
                .HasMaxLength(100)
                .IsRequired();

            info.Property(pi => pi.Email)
                .HasMaxLength(256)
                .IsRequired();

            info.Property(pi => pi.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();
        });

        builder.Property(d => d.Specialization)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.LicenseNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(d => d.IsActive)
            .IsRequired();
    }
}

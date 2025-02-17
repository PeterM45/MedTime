namespace MedTime.Infrastructure.Persistence.Configurations;

using MedTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.Id);

        builder.OwnsOne(p => p.PersonInfo, info =>
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

        builder.Property(p => p.DateOfBirth)
            .IsRequired();
    }
}

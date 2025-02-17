namespace MedTime.Domain.Entities;

using MedTime.Domain.Common;
using MedTime.Domain.Exceptions;
using MedTime.Domain.ValueObjects;

public class Doctor : Entity
{
    public PersonInfo PersonInfo { get; private set; }
    public string Specialization { get; private set; }
    public string LicenseNumber { get; private set; }
    public bool IsActive { get; private set; }

#pragma warning disable CS8618 // Disable warning for EF Core constructor
    private Doctor() { } // Required by EF Core
#pragma warning restore CS8618

    public Doctor(
        string firstName,
        string lastName,
        string specialization,
        string licenseNumber,
        string email,
        string phoneNumber)
        : base()
    {
        ValidateDoctor(specialization, licenseNumber);

        PersonInfo = PersonInfo.Create(firstName, lastName, email, phoneNumber);
        Specialization = specialization;
        LicenseNumber = licenseNumber;
        IsActive = true;
    }

    public void UpdateContactInformation(string email, string phoneNumber)
    {
        PersonInfo = PersonInfo.Create(
            PersonInfo.FirstName,
            PersonInfo.LastName,
            email,
            phoneNumber);
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    private static void ValidateDoctor(string specialization, string licenseNumber)
    {
        if (string.IsNullOrWhiteSpace(specialization))
            throw new DomainException("Specialization is required");

        if (string.IsNullOrWhiteSpace(licenseNumber))
            throw new DomainException("License number is required");
    }
}

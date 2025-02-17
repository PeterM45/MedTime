namespace MedTime.Domain.Entities;

using MedTime.Domain.Common;
using MedTime.Domain.Exceptions;
using MedTime.Domain.ValueObjects;

public class Patient : Entity
{
    public PersonInfo PersonInfo { get; private set; }
    public DateOnly DateOfBirth { get; private set; }

    private Patient() { } // Required by EF Core

    public Patient(string firstName, string lastName, DateOnly dateOfBirth, string email, string phoneNumber)
        : base()
    {
        ValidateDateOfBirth(dateOfBirth);

        PersonInfo = PersonInfo.Create(firstName, lastName, email, phoneNumber);
        DateOfBirth = dateOfBirth;
    }

    public void UpdateContactInformation(string email, string phoneNumber)
    {
        PersonInfo = PersonInfo.Create(
            PersonInfo.FirstName,
            PersonInfo.LastName,
            email,
            phoneNumber);
    }

    private static void ValidateDateOfBirth(DateOnly dateOfBirth)
    {
        if (dateOfBirth >= DateOnly.FromDateTime(DateTime.UtcNow))
            throw new DomainException("Date of birth cannot be in the future");
    }
}

namespace MedTime.Domain.ValueObjects;

using System.Text.RegularExpressions;
using MedTime.Domain.Exceptions;

public record PersonInfo
{
    private static readonly Regex EmailPattern = new(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static readonly Regex PhonePattern = new(
        @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
        RegexOptions.Compiled);

    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }

    private PersonInfo(string firstName, string lastName, string email, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = FormatPhoneNumber(phoneNumber);
    }

    public static PersonInfo Create(string firstName, string lastName, string email, string phoneNumber)
    {
        Validate(firstName, lastName, email, phoneNumber);
        return new PersonInfo(firstName, lastName, email, phoneNumber);
    }

    private static void Validate(string firstName, string lastName, string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("First name is required");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Last name is required");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email is required");

        if (!EmailPattern.IsMatch(email))
            throw new DomainException("Invalid email format");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new DomainException("Phone number is required");

        if (!PhonePattern.IsMatch(phoneNumber))
            throw new DomainException("Invalid phone number format. Use: (123) 456-7890, 123-456-7890, or 1234567890");
    }

    private static string FormatPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber)) return phoneNumber;

        var digits = new string(phoneNumber.Where(char.IsDigit).ToArray());

        return digits.Length == 10
            ? $"({digits[..3]}) {digits.Substring(3, 3)}-{digits[6..]}"
            : phoneNumber;
    }

    public string FullName => $"{FirstName} {LastName}";
}

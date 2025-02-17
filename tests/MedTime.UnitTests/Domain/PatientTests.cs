namespace MedTime.UnitTests.Domain;

using FluentAssertions;
using MedTime.Domain.Entities;
using MedTime.Domain.Exceptions;
using Xunit;

public class PatientTests
{
	[Fact]
	public void CreatePatient_WithValidData_ShouldSucceed()
	{
		// Arrange
		var firstName = "John";
		var lastName = "Doe";
		var dateOfBirth = new DateOnly(1990, 1, 1);
		var email = "john@example.com";
		var phoneNumber = "(555) 123-4567";

		// Act
		var patient = new Patient(firstName, lastName, dateOfBirth, email, phoneNumber);

		// Assert
		patient.Should().NotBeNull();
		patient.Id.Should().NotBe(Guid.Empty);
		patient.PersonInfo.FirstName.Should().Be(firstName);
		patient.PersonInfo.LastName.Should().Be(lastName);
		patient.DateOfBirth.Should().Be(dateOfBirth);
		patient.PersonInfo.Email.Should().Be(email);
		patient.PersonInfo.PhoneNumber.Should().Be(phoneNumber);
	}

	[Fact]
	public void CreatePatient_WithFutureDate_ShouldThrowException()
	{
		// Arrange
		var futureDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));

		// Act
		var act = () => new Patient("John", "Doe", futureDate, "john@example.com", "(555) 123-4567");

		// Assert
		act.Should().Throw<DomainException>()
			.WithMessage("Date of birth cannot be in the future");
	}

	[Theory]
	[InlineData("", "Doe", "Invalid first name")]
	[InlineData("John", "", "Invalid last name")]
	[InlineData("John", "Doe", "invalid-email")]
	public void CreatePatient_WithInvalidData_ShouldThrowException(string firstName, string lastName, string email)
	{
		// Arrange
		var dateOfBirth = new DateOnly(1990, 1, 1);

		// Act
		var act = () => new Patient(firstName, lastName, dateOfBirth, email, "(555) 123-4567");

		// Assert
		act.Should().Throw<DomainException>();
	}
}
namespace MedTime.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using MedTime.Application.Common.Interfaces;
using MedTime.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
	private readonly IPatientRepository _patientRepository;

	public PatientsController(IPatientRepository patientRepository)
	{
		_patientRepository = patientRepository;
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> Create(CreatePatientRequest request)
	{
		var patient = new Patient(
			request.FirstName,
			request.LastName,
			request.DateOfBirth,
			request.Email,
			request.PhoneNumber);

		await _patientRepository.AddAsync(patient, CancellationToken.None);
		return Ok(patient.Id);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<PatientResponse>> Get(Guid id)
	{
		var patient = await _patientRepository.GetByIdAsync(id, CancellationToken.None);

		if (patient == null)
			return NotFound();

		return Ok(new PatientResponse(
			patient.Id,
			patient.PersonInfo.FirstName,
			patient.PersonInfo.LastName,
			patient.DateOfBirth,
			patient.PersonInfo.Email,
			patient.PersonInfo.PhoneNumber));
	}
}

public record CreatePatientRequest(
	string FirstName,
	string LastName,
	DateOnly DateOfBirth,
	string Email,
	string PhoneNumber);

public record PatientResponse(
	Guid Id,
	string FirstName,
	string LastName,
	DateOnly DateOfBirth,
	string Email,
	string PhoneNumber);
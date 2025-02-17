namespace MedTime.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using MedTime.Application.Common.Interfaces;
using MedTime.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
	private readonly IDoctorRepository _doctorRepository;

	public DoctorsController(IDoctorRepository doctorRepository)
	{
		_doctorRepository = doctorRepository;
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> Create(CreateDoctorRequest request)
	{
		var doctor = new Doctor(
			request.FirstName,
			request.LastName,
			request.Specialization,
			request.LicenseNumber,
			request.Email,
			request.PhoneNumber);

		await _doctorRepository.AddAsync(doctor, CancellationToken.None);
		return Ok(doctor.Id);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<DoctorResponse>> Get(Guid id)
	{
		var doctor = await _doctorRepository.GetByIdAsync(id, CancellationToken.None);

		if (doctor == null)
			return NotFound();

		return Ok(new DoctorResponse(
			doctor.Id,
			doctor.PersonInfo.FirstName,
			doctor.PersonInfo.LastName,
			doctor.Specialization,
			doctor.LicenseNumber,
			doctor.PersonInfo.Email,
			doctor.PersonInfo.PhoneNumber,
			doctor.IsActive));
	}

	[HttpGet("active")]
	public async Task<ActionResult<IEnumerable<DoctorResponse>>> GetActive()
	{
		var doctors = await _doctorRepository.GetActiveAsync(CancellationToken.None);

		return Ok(doctors.Select(d => new DoctorResponse(
			d.Id,
			d.PersonInfo.FirstName,
			d.PersonInfo.LastName,
			d.Specialization,
			d.LicenseNumber,
			d.PersonInfo.Email,
			d.PersonInfo.PhoneNumber,
			d.IsActive)));
	}
}

public record CreateDoctorRequest(
	string FirstName,
	string LastName,
	string Specialization,
	string LicenseNumber,
	string Email,
	string PhoneNumber);

public record DoctorResponse(
	Guid Id,
	string FirstName,
	string LastName,
	string Specialization,
	string LicenseNumber,
	string Email,
	string PhoneNumber,
	bool IsActive);
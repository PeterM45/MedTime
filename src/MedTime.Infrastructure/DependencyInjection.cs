namespace MedTime.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MedTime.Application.Common.Interfaces;
using MedTime.Infrastructure.Persistence;
using MedTime.Infrastructure.Persistence.Repositories;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlite(
				configuration.GetConnectionString("DefaultConnection")));

		services.AddScoped<IAppointmentRepository, AppointmentRepository>();
		services.AddScoped<IPatientRepository, PatientRepository>();
		services.AddScoped<IDoctorRepository, DoctorRepository>();

		return services;
	}
}

namespace MedTime.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using MedTime.Domain.Entities;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<Patient> Patients => Set<Patient>();
	public DbSet<Doctor> Doctors => Set<Doctor>();
	public DbSet<Appointment> Appointments => Set<Appointment>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
	}
}
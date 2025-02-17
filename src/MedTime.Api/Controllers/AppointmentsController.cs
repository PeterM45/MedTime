namespace MedTime.Api.Controllers;

using MediatR;
using MedTime.Application.Features.Appointments.Commands.ScheduleAppointment;
using MedTime.Application.Features.Appointments.Queries.GetAppointmentById;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Schedule(ScheduleAppointmentCommand command)
    {
        var appointmentId = await _mediator.Send(command);
        return Ok(appointmentId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentDto>> Get(Guid id)
    {
        var query = new GetAppointmentByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

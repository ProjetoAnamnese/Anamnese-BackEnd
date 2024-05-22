using Anamnese.API.Application.Services.Appointment;
using Anamnese.API.ORM.Model.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anamnese.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("get-appointment-by-pacient/{pacientId}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProfissionaltsById(int pacientId)
        {
            var appointment = _appointmentService.GetSpecialityByPacient(pacientId);

            if (appointment != null)
            {
                return Ok(appointment);
            }
            else
            {
                return BadRequest("appointment não encontrado");
            }
        }

        [HttpGet("profissional-appointments/{profissionalId}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProfissionalApppointment(int profissionalId)
        {
            var appointment = _appointmentService.GetAppointmentByProfissional(profissionalId);

            if (appointment != null)
            {
                return Ok(appointment);
            }
            else
            {
                return BadRequest("appointment não encontrado");
            }
        }

        [HttpPost("schedule-appointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ScheduleAppointment([FromBody] AppointmentRequestModel appointmentRequest)
        {
            if (appointmentRequest == null)
            {
                return BadRequest("Invalid appointment request");
            }

            bool isScheduled = _appointmentService.ScheduleAppointment(appointmentRequest.ProfissionalId, appointmentRequest.PacientId, appointmentRequest.AppointmentDate, appointmentRequest.AppointmentTime);

            if (isScheduled)
            {
                return Ok();
            }
            else
            {
                return BadRequest("O profissional não esta disponivel");
            }
        }
    }

}


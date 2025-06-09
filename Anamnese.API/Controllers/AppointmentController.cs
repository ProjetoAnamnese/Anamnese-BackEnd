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
        [Authorize]
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

        [HttpGet("profissional-appointments")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProfissionalAppointment(
          [FromQuery] int pageNumber = 1,
          [FromQuery] int pageSize = 10)
        {
            var appointments = _appointmentService.GetAppointmentByProfissional(pageNumber, pageSize);
            return Ok(appointments);
        }



        [HttpPost("schedule-appointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ScheduleAppointment([FromBody] AppointmentRequestModel appointmentRequest)
        {
            if (appointmentRequest == null)
            {
                return Ok(new
                {
                    success = false,
                    message = "Requisição inválida."
                });
            }

            bool isScheduled = _appointmentService.ScheduleAppointment(
                appointmentRequest.PacientId,
                appointmentRequest.AppointmentDate,
                appointmentRequest.AppointmentTime
            );

            if (isScheduled)
            {
                return Ok(new
                {
                    success = true,
                    message = "Agendamento realizado com sucesso."
                });
            }
            else
            {
                return Ok(new
                {
                    success = false,
                    message = "O profissional não está disponível no horário selecionado."
                });
            }
        }

        [HttpGet("next-of-day")]
        public IActionResult GetNextAppointmentsOfDay()
        {
            var result = _appointmentService.GetNextAppointmentsOfDay();
            return Ok(result);
        }


    }

}


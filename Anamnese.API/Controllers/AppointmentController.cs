using Anamnese.API.Application.Services.Appointment;
using Anamnese.API.ORM.Filters;
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
          [FromQuery] AppointmentFilter filters,
          [FromQuery] int pageNumber = 1,
          [FromQuery] int pageSize = 10
          )
        {
            var appointments = _appointmentService.GetAppointmentByProfissional(filters, pageNumber, pageSize );
            return Ok(appointments);
        }



        [HttpPatch("update-appointment/{appointmentId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateAppointment(int appointmentId, UpdateAppointmentModel updateAppointmentModel)
        {
            try
            {
                var updatedAppointment = _appointmentService.UpdateAppointment(appointmentId, updateAppointmentModel);
                return Ok(updatedAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }

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

            string message;

            bool isScheduled = _appointmentService.ScheduleAppointment(
                appointmentRequest.PacientId,
                appointmentRequest.AppointmentDate,
                appointmentRequest.AppointmentTime,
                out message
            );

            return Ok(new
            {
                success = isScheduled,
                message = message
            });
        }




        [HttpGet("next-of-day")]
        public IActionResult GetNextAppointmentsOfDay()
        {
            var result = _appointmentService.GetNextAppointmentsOfDay();
            return Ok(result);
        }


    }

}


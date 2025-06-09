using Anamnese.API.Application.Services.Pacient;
using Anamnese.API.ORM.Filters;
using Anamnese.API.ORM.Model.PacientModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anamnese.API.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class PacientController : ControllerBase
    {
        private readonly IPacientService _pacientService;

        public PacientController(IPacientService pacientService)
        {
            _pacientService = pacientService;
        }

        /// <summary>
        /// Retorna todos os pacientes com base nos filtros.
        /// </summary>
        /// <param name="filter">Filtros de pesquisa</param>
        /// <returns>Lista de pacientes</returns>
        [HttpGet("get-pacients")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPacients([FromQuery] PacientFilter filter)
        {
            var pacients = _pacientService.GetAllPacients(filter);
            return Ok(pacients);
        }

        /// <summary>
        /// Retorna um paciente específico pelo ID.
        /// </summary>
        /// <param name="pacientId">ID do paciente</param>
        /// <returns>Paciente encontrado ou erro</returns>
        [HttpGet("get-pacient/{pacientId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPacientsById(int pacientId)
        {
            var pacient = _pacientService.GetPacientById(pacientId);
            return pacient != null ? Ok(pacient) : BadRequest("Paciente não encontrado");
        }

        /// <summary>
        /// Retorna os pacientes vinculados ao profissional autenticado.
        /// </summary>
        /// <returns>Lista de pacientes do profissional</returns>
        [HttpGet("get-profissional-pacient")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProfissionalPacients()
        {
            var pacients = _pacientService.GetPacientsByProfissional();
            return pacients != null ? Ok(pacients) : BadRequest("Pacientes não encontrados para o profissional especificado");
        }


        [HttpGet("count-by-report")]
        public IActionResult CountByReport()
        {
            var result = _pacientService.CountPacientsWithAndWithoutReports();
            return Ok(result);
        }


        /// <summary>
        /// Retorna a contagem de pacientes por especialidade.
        /// </summary>
        /// <returns>Contagem agrupada por especialidade</returns>
        [HttpGet("count-pacient-by-specialty")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CountReferralsBySpecialty()
        {
            var pacientCount = _pacientService.CountPacientBySpecialty();
            return Ok(pacientCount);
        }

        /// <summary>
        /// Cria um novo paciente.
        /// </summary>
        /// <param name="pacientModel">Dados do novo paciente</param>
        /// <returns>Retorna o paciente criado ou erro</returns>
        [HttpPost("create-pacient")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreatePacient([FromBody] CreatePacientRequest pacientModel)
        {
            if (pacientModel == null)
                return BadRequest("Dados do paciente inválidos");

            var result = _pacientService.CreatePacient(pacientModel);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }

        /// <summary>
        /// Atualiza os dados de um paciente existente.
        /// </summary>
        /// <param name="pacientId">ID do paciente</param>
        /// <param name="updatedPacientModel">Dados atualizados do paciente</param>
        /// <returns>Retorna o paciente atualizado ou erro</returns>
        [HttpPut("update-pacient/{pacientId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePacient(int pacientId, [FromBody] UpdatePacientRequest updatedPacientModel)
        {
            if (updatedPacientModel == null)
                return BadRequest("Dados do paciente inválidos");

            var result = _pacientService.UpdatePacient(pacientId, updatedPacientModel);

            if (!result.Success)
                return NotFound(new { message = result.Message });

            return Ok(result.Data);
        }

        /// <summary>
        /// Remove um paciente pelo ID.
        /// </summary>
        /// <param name="pacientId">ID do paciente a ser removido</param>
        /// <returns>Confirmação da remoção</returns>
        [HttpDelete("remove-pacient/{pacientId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RemovePacient(int pacientId)
        {
            var removedPacient = _pacientService.DeletePacient(pacientId);
            return Ok(removedPacient);
        }

        /// <summary>
        /// Retorna o número total de pacientes cadastrados.
        /// </summary>
        /// <returns>Total de pacientes</returns>
        [HttpGet("count-pacients")]
        public IActionResult CountAllPacients()
        {
            int totalPacients = _pacientService.CountAllPacients();
            return Ok(totalPacients);
        }

        /// <summary>
        /// Retorna o número total de pacientes vinculados a profissionais.
        /// </summary>
        /// <returns>Total de pacientes por profissional</returns>
        [HttpGet("count-profissional")]
        public IActionResult CountProfissionalPacients()
        {
            int totalProfissionalPacients = _pacientService.CountAllProfissionalPacients();
            return Ok(totalProfissionalPacients);
        }
    }
}

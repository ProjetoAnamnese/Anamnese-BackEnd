using Anamnese.API.Application.Services.Pacient;
using Anamnese.API.Application.Services.Report;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Filters;
using Anamnese.API.ORM.Model.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anamnese.API.Controllers
{
    /// <summary>
    /// Controller responsável por operações relacionadas a fichas de anamnese (relatórios).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IPacientService _pacientService;

        public ReportController(IReportService reportService, IPacientService pacientService)
        {
            _reportService = reportService;
            _pacientService = pacientService;
        }

        /// <summary>
        /// Retorna todas as fichas com base nos filtros fornecidos.
        /// </summary>
        /// <param name="filters">Filtros de pesquisa</param>
        /// <returns>Lista de fichas</returns>
        [HttpGet("get-reports")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllReports([FromQuery] ReportFilter filters)
        {
            var report = _reportService.GetAllReports(filters);
            return Ok(report);
        }

        /// <summary>
        /// Retorna a ficha de anamnese vinculada a um paciente específico.
        /// </summary>
        /// <param name="pacientId">ID do paciente</param>
        /// <returns>Ficha do paciente</returns>
        [HttpGet("get-pacient-report/{pacientId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetReportByPacientId(int pacientId)
        {
            var report = _reportService.GetReportByPacientId(pacientId);
            return report != null ? Ok(report) : BadRequest("Relatório do paciente não encontrado");
        }

        /// <summary>
        /// Retorna uma ficha específica pelo ID.
        /// </summary>
        /// <param name="reportId">ID da ficha</param>
        /// <returns>Ficha encontrada</returns>
        [HttpGet("get-report/{reportId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetReportById(int reportId)
        {
            var report = _reportService.GetReportById(reportId);
            return report != null ? Ok(report) : BadRequest("Ficha não encontrada");
        }

        /// <summary>
        /// Cria uma nova ficha de anamnese para um paciente.
        /// </summary>
        /// <param name="reportModel">Dados da ficha</param>
        /// <param name="pacientId">ID do paciente</param>
        /// <returns>Ficha criada ou erro</returns>
        [HttpPost("create-report/{pacientId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateReport([FromBody] CreateReportRequest reportModel, int pacientId)
        {
            if (reportModel == null)
                return BadRequest("Dados da ficha inválida");
            var result = _reportService.CreateReport(pacientId, reportModel);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }

        [HttpGet("count-by-month")]
        public IActionResult CountReportsByMonth()
        {
            var result = _reportService.CountReportsByMonth();
            return Ok(result);
        }


        /// <summary>
        /// Atualiza uma ficha de anamnese existente.
        /// </summary>
        /// <param name="reportId">ID da ficha</param>
        /// <param name="updatedReportModel">Dados atualizados da ficha</param>
        /// <returns>Ficha atualizada ou erro</returns>
        [HttpPut("update-report/{reportId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateReport(int reportId, [FromBody] UpdateReportRequest updatedReportModel)
        {
            var existingReport = _reportService.GetReportById(reportId);
            if (existingReport == null)
                return BadRequest("Ficha não encontrada.");

            var result = _reportService.UpdateReport(reportId, updatedReportModel);
            if (!result.Success)
                return NotFound(new { message = result.Message });

            return Ok(result.Data);
        }

        /// <summary>
        /// Remove uma ficha de anamnese pelo ID.
        /// </summary>
        /// <param name="reportId">ID da ficha</param>
        /// <returns>Confirmação da remoção</returns>
        [HttpDelete("delete-report/{reportId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteReport(int reportId)
        {
            var removeReport = _reportService.DeleteReport(reportId);
            return Ok(removeReport);
        }

        /// <summary>
        /// Retorna o número total de fichas de anamnese cadastradas.
        /// </summary>
        /// <returns>Contagem total de fichas</returns>
        [HttpGet("count-report")]
        public IActionResult CountAllReports()
        {
            var count = _reportService.CountAllReports();
            return Ok(count);
        }
    }
}

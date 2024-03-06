using AnamneseAPI.Models;
using AnamneseAPI.Services.Pacient.Models;
using AnamneseAPI.Services.Report;
using AnamneseAPI.Services.Report.Models;
using CatalogAPI.Models;
using CatalogAPI.Services.Pacient;
using CatalogAPI.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnamneseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IPacientService _pacientService;
        //private readonly ITokenService _tokenService;
        public ReportController(IReportService reportService, ITokenService tokenService, IPacientService pacientService)
        {
            _reportService = reportService;
            _pacientService = pacientService;
            //_tokenService = tokenService;
        }

        [HttpPost("create-report/{pacientId}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateReport([FromBody] CreateReportRequest reportModel, int pacientId)
        {
            //pacientModel.DoctorId = _tokenService.GetUserId();
            var existsPacients = _pacientService.PacientExists(pacientId);
            if (reportModel == null || pacientId == null || !existsPacients)
            {
                return BadRequest();
            }

            var createdPacient = _reportService.CreateReport(pacientId, reportModel);
            if (createdPacient != null)
            {
                return Ok(reportModel);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("get-reports")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllReports()
        {
            var report = _reportService.GetAllReports();

            return Ok(report);
        }

        [HttpGet("get-report/{reportId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetReportById(int reportId)
        {
            var report = _reportService.GetReportById(reportId);

            if (report != null)
            {
                return Ok(report);
            }
            else
            {
                return BadRequest("Ficha não encontrado");
            }
        }

        [HttpGet("get-pacient-report/{pacientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetReportByPacientId(int reportId)
        {
            var report = _reportService.GetReportById(reportId);

            if (report != null)
            {
                return Ok(report);
            }
            else
            {
                return BadRequest("Ficha não encontrado");
            }
        }

        [HttpDelete("remove-report/{reportId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RemoveReport(int reportId)
        {
            var removedReport = _reportService.DeleteReport(reportId);

            return Ok(removedReport);

        }

        //[HttpPut("update-report/{reportId}")]
        //[Authorize]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult UpdateReport(int reportId, [FromBody] ReportModel updatedReportModel)
        //{
            
        //}
    }
}

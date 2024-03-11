using Anamnese.API.Application.Services.Report;
using Anamnese.API.ORM.Model.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anamnese.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;            
        }


        [HttpGet("get-reports")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllReports()
        {
            var report = _reportService.GetAllReports();

            return Ok(report);
        }


        //Pegar a ficha do paciente especifico
        [HttpGet("get-pacient-report/{pacientId}")]
        [Authorize]
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

        [HttpGet("get-report/{reportId}")]
        [Authorize]
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

        [HttpPost("create-report/{pacientId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateReport([FromBody] CreateReportRequest reportModel, int pacientId)
        {

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


    }
}

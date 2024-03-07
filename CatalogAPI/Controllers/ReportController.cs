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

        [HttpGet("get-reports")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllReports()
        {
            var report = _reportService.GetAllReports();

            return Ok(report);
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

        [HttpPut("update-report/{reportId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateReport(int reportId, [FromBody] CreateReportRequest updatedReportModel)
        {
            if (updatedReportModel == null)
            {
                return BadRequest("Dados inválidos");
            }

            var existingReport = _reportService.GetReportById(reportId);
            if (existingReport == null)
            {
                return BadRequest("Ficha não encontrada");
            }            

            existingReport.MedicalHistory = updatedReportModel.MedicalHistory;
            existingReport.CurrentMedications = updatedReportModel.CurrentMedications;
            existingReport.CardiovascularIssues = updatedReportModel.CardiovascularIssues;
            existingReport.Diabetes = updatedReportModel.Diabetes;
            existingReport.FamilyHistoryCardiovascularIssues = updatedReportModel.FamilyHistoryCardiovascularIssues;
            existingReport.FamilyHistoryDiabetes = updatedReportModel.FamilyHistoryDiabetes;
            existingReport.PhysicalActivity = updatedReportModel.PhysicalActivity;
            existingReport.Smoker = updatedReportModel.Smoker;
            existingReport.ReportDateTime = DateTime.Now;
            existingReport.AlcoholConsumption = updatedReportModel.AlcoholConsumption;
            existingReport.EmergencyContactName = updatedReportModel.EmergencyContactName;
            existingReport.EmergencyContactPhone = updatedReportModel.EmergencyContactPhone;
            existingReport.Observations = updatedReportModel.Observations;
            existingReport.MedicalHistory = updatedReportModel.MedicalHistory;
            existingReport.CurrentMedications = updatedReportModel.CurrentMedications;

            var updatedReport = _reportService.UpdateReport(reportId, existingReport);

            if (updatedReport != null)
            {
                return Ok(updatedReport);
            }
            else
            {
                return BadRequest("Falha ao atualizar a ficha");
            }
        }
    }
}

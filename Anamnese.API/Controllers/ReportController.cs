using Anamnese.API.Application.Services.Pacient;
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
        private readonly IPacientService _pacientService;
        public ReportController(IReportService reportService, IPacientService pacientService)
        {
            _reportService = reportService;            
            _pacientService = pacientService;
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
        public IActionResult GetReportByPacientId(int pacientId)
        {
            var report = _reportService.GetReportByPacientId(pacientId);

            if (report != null)
            {
                return Ok(report);
            }
            else
            {
                return BadRequest("Relatório do paciente não encontrado");
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

        [HttpPut("update-report/{reportId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateReport(int reportId, [FromBody] CreateReportRequest updatedReportModel)
        {
            var existingReport = _reportService.GetReportById(reportId);

            if (existingReport == null)
            {
                return BadRequest("Ficha não encontrada.");
            }

            // Atualiza apenas os campos fornecidos no modelo de atualização
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

            var updatedReport = _reportService.UpdateReport(reportId, existingReport);

            if (updatedReport != null)
            {
                return Ok(updatedReport);
            }
            else
            {
                return BadRequest("Falha ao atualizar a ficha.");
            }
        }
        [HttpDelete("delete-report/{reportId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteReport(int reportId)
        {
            var removeReport = _reportService.DeleteReport(reportId);

            return Ok(removeReport); 
            
        }
        [HttpGet("count-report")]
        public IActionResult CountAllReports()
        {
            var count = _reportService.CountAllReports();
            return Ok(count);
        }



    }
}

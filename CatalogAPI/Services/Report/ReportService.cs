using AnamneseAPI.Models;
using AnamneseAPI.Services.Report.Models;
using CatalogAPI.Context;
using CatalogAPI.Models;
using CatalogAPI.Repository;
using CatalogAPI.Services.Pacient;
using CatalogAPI.Services.Token;

namespace AnamneseAPI.Services.Report
{
    public class ReportService : IReportService
    {        
        private readonly MySQLContext _context;
        private readonly BaseRepository<ReportModel> _reportRepository;
        private IPacientService _pacientService;
        private ITokenService _tokenService { get; }



        public ReportService(MySQLContext context, BaseRepository<ReportModel> reportRepository, ITokenService tokenService, IPacientService pacientService)
        {
            _context = context;
            _reportRepository = reportRepository;
            _tokenService = tokenService;
            _pacientService = pacientService;
        }

        public IEnumerable<ReportModel> GetAllReports()
        {
            return _reportRepository.GetAll();
        }


        public ReportModel GetReportById(int id)
        {            
            return _reportRepository.GetById(id);
        }


        public ReportModel CreateReport(int pacientId, CreateReportRequest report)
        {
            if(report == null) throw new Exception("Erro ao criar ficha");
            var paciente = _pacientService.GetPacientById(pacientId);
            ReportModel newReport = new ReportModel
            {
               PacientId = pacientId,
               MedicalHistory = report.MedicalHistory,
               CurrentMedications = report.CurrentMedications,
               CardiovascularIssues = report.CardiovascularIssues,
               Diabetes = report.Diabetes,
               FamilyHistoryCardiovascularIssues = report.FamilyHistoryCardiovascularIssues,
               FamilyHistoryDiabetes = report.FamilyHistoryDiabetes,
               PhysicalActivity = report.PhysicalActivity,
               Smoker = report.Smoker,
               AlcoholConsumption = report.AlcoholConsumption,
               EmergencyContactName = report.EmergencyContactName,
               EmergencyContactPhone = report.EmergencyContactPhone,
               Observations = report.Observations,                          
            };
            _reportRepository.Add(newReport);
            _reportRepository.SaveChanges();
            _pacientService.PatchPacient(pacientId, newReport.Id);
            return newReport;            
            
        }

        public ReportModel UpdateReport(int id, ReportModel updatedReport)
        {
            var existingReport = _reportRepository.GetById(id);  
            //var existingReport = _reports.FirstOrDefault(r => r.Id == id);
            if (existingReport != null)
            {
                existingReport.MedicalHistory = updatedReport.MedicalHistory;
                existingReport.CurrentMedications = updatedReport.CurrentMedications;
                existingReport.CardiovascularIssues = updatedReport.CardiovascularIssues;
                existingReport.Diabetes = updatedReport.Diabetes;
                existingReport.FamilyHistoryCardiovascularIssues = updatedReport.FamilyHistoryCardiovascularIssues;
                existingReport.FamilyHistoryDiabetes = updatedReport.FamilyHistoryDiabetes;
                existingReport.PhysicalActivity = updatedReport.PhysicalActivity;
                existingReport.Smoker = updatedReport.Smoker;
                existingReport.AlcoholConsumption = updatedReport.AlcoholConsumption;
                existingReport.EmergencyContactName = updatedReport.EmergencyContactName;
                existingReport.EmergencyContactPhone = updatedReport.EmergencyContactPhone;
                existingReport.Observations = updatedReport.Observations;
                existingReport.MedicalHistory = updatedReport.MedicalHistory;
                existingReport.CurrentMedications = updatedReport.CurrentMedications;
                return _reportRepository.Update(existingReport);                
            }
            return null;
        }

        public ReportModel DeleteReport(int id)
        {
            var reportToRemove = _reportRepository.GetById(id);        
            if (reportToRemove != null)
            {
                _reportRepository.Delete(reportToRemove);
                _context.SaveChanges();
            }
            return null;
        }           
    }
}

using Anamnese.API.Application.Services.Pacient;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.Report;
using Anamnese.API.ORM.Repository;
using Microsoft.EntityFrameworkCore;

namespace Anamnese.API.Application.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly BaseRepository<ReportModel> _reportRepository;
        private IPacientService _pacientService;

        public ReportService(BaseRepository<ReportModel> reportRepository, IPacientService pacientService)
        {         
            _reportRepository = reportRepository;   
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
            var existsPacients = _pacientService.PacientExists(pacientId);
            if (report == null || pacientId == null || !existsPacients)
            {
                return null;
            }
            var pacient = _pacientService.GetPacientById(pacientId);
            ReportModel reportModel = new ReportModel
            {
                PacientId = pacientId,
                MedicalHistory = report.MedicalHistory,
                ReportDateTime = DateTime.Now,
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
                PacientName = pacient.Username,
                Pacient = pacient
            };
            _reportRepository.Add(reportModel);
            _reportRepository.SaveChanges();
            return reportModel;


        }
        public ReportModel UpdateReport(int id, ReportModel updatedReport)
        {
            var existingReport = _reportRepository.GetById(id);            
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
                existingReport.ReportDateTime = DateTime.Now;
                existingReport.AlcoholConsumption = updatedReport.AlcoholConsumption;
                existingReport.EmergencyContactName = updatedReport.EmergencyContactName;
                existingReport.EmergencyContactPhone = updatedReport.EmergencyContactPhone;
                existingReport.Observations = updatedReport.Observations;
                existingReport.MedicalHistory = updatedReport.MedicalHistory;
                existingReport.CurrentMedications = updatedReport.CurrentMedications;

                _reportRepository.Update(existingReport);
                _reportRepository.SaveChanges();
                return existingReport;
            }
            return null;
        }

        public ReportModel DeleteReport(int id)
        {
            var reportToRemove = _reportRepository.GetById(id);
            if (reportToRemove != null)
            {
                _reportRepository.Delete(reportToRemove);
                _reportRepository.SaveChanges();
            }
            return null;
        }

        public int CountAllReports()
        {
            return _reportRepository.Count();
        }
    }
}

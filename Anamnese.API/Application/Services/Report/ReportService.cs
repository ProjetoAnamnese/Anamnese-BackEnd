using Anamnese.API.Application.Services.Pacient;
using Anamnese.API.Application.Services.Token;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Filters;
using Anamnese.API.ORM.Model.Common;
using Anamnese.API.ORM.Model.Report;
using Anamnese.API.ORM.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anamnese.API.Application.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly BaseRepository<ReportModel> _reportRepository;
        private ITokenService _tokenService { get; }

        private IPacientService _pacientService;
        private IMapper _mapper;

        public ReportService(BaseRepository<ReportModel> reportRepository, IPacientService pacientService, ITokenService tokenService, IMapper mapper)

        {
            _tokenService = tokenService;
            _reportRepository = reportRepository;   
            _pacientService = pacientService;
            _mapper = mapper;
        }

        public PagedResponse<ReportModel> GetAllReports(ReportFilter filters)
        {
            var profissionalId = _tokenService.GetUserId();
            var query = _reportRepository._context.Report
              .Include(r => r.Pacient)
              .Where(r => r.Pacient.ProfissionalId == profissionalId);

            if (filters.PacientId.HasValue && filters.PacientId.Value > 0)
            {
                query = query.Where(r => r.PacientId == filters.PacientId.Value);
            }


            if (filters.CardiovascularIssues.HasValue)
                query = query.Where(r => r.CardiovascularIssues == filters.CardiovascularIssues.Value);

            if (filters.Smoker.HasValue)
                query = query.Where(r => r.Smoker == filters.Smoker.Value);


            if (filters.Diabates.HasValue)
                query = query.Where(r => r.Smoker == filters.Diabates.Value);



            var totalCount = query.Count();
            var items = query
               .Skip((filters.PageNumber - 1) * filters.PageSize)
               .Take(filters.PageSize)
               .ToList();


            return new PagedResponse<ReportModel>
            {
                Items = items,
                TotalCount = totalCount,
                PerPage = filters.PageSize
            };
        }

        public Result<ReportResponseModel> CreateReport(int pacientId, CreateReportRequest report)
        {
            var existingReport = GetReportByPacientId(pacientId);
            if (existingReport != null)
            {
                return null;
            }
            var pacient = _pacientService.GetPacientById(pacientId);
            var newReport = _mapper.Map<ReportModel>(report);
            newReport.PacientId = pacientId;
            newReport.PacientName = pacient.Username;
            newReport.ReportDateTime = DateTime.Now;
            var res = _reportRepository.Add(newReport);
            _reportRepository.SaveChanges();
            var mapped = _mapper.Map<ReportResponseModel>(res);
            return Result<ReportResponseModel>.Ok(mapped);
        }

        public IEnumerable<ReportModel> GetAllReports()
        {
            return _reportRepository.GetAll();
        }

        public ReportModel GetReportById(int id)
        {
            return _reportRepository.GetById(id);          

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
        public ReportModel GetReportByPacientId(int pacientId)
        {
         
            var report = _reportRepository.GetAll().FirstOrDefault(r => r.PacientId == pacientId);

            return report;
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

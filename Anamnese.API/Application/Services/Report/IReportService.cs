using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.Report;

namespace Anamnese.API.Application.Services.Report
{
    public interface IReportService
    {
        IEnumerable<ReportModel> GetAllReports();
        ReportModel GetReportById(int id);
        ReportModel GetReportByPacientId(int pacientId);

        ReportModel CreateReport(int pacientId, CreateReportRequest report);
        ReportModel UpdateReport(int id, ReportModel updatedReport);
        
        ReportModel DeleteReport(int id);
        int CountAllReports();
    }
}

using AnamneseAPI.Models;
using AnamneseAPI.Services.Pacient.Models;
using AnamneseAPI.Services.Report.Models;
using CatalogAPI.Models;

namespace AnamneseAPI.Services.Report
{
    public interface IReportService
    {
        IEnumerable<ReportModel> GetAllReports();
        ReportModel GetReportById(int id);
        ReportModel CreateReport(int pacientId,CreateReportRequest report);
        ReportModel UpdateReport(int id, ReportModel updatedReport);
        //ReportModel UpdateReport(int id, CreateReportRequest updatedReport);
        ReportModel DeleteReport(int id);
    }
}

using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Filters;
using Anamnese.API.ORM.Model.Common;
using Anamnese.API.ORM.Model.Report;

namespace Anamnese.API.Application.Services.Report
{
    public interface IReportService
    {        
        PagedResponse<ReportModel> GetAllReports(ReportFilter filters);
        Result<ReportResponseModel> CreateReport(int pacientId, CreateReportRequest report);

        ReportModel GetReportById(int id);
        ReportModel GetReportByPacientId(int pacientId);

        
        ReportModel UpdateReport(int id, ReportModel updatedReport);
        
        ReportModel DeleteReport(int id);
        int CountAllReports();
    }
}

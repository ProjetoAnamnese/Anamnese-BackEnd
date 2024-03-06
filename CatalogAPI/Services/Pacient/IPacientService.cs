using AnamneseAPI.Models;
using AnamneseAPI.Services.Pacient.Models;
using AnamneseAPI.Services.Report.Models;
using CatalogAPI.Models;

namespace CatalogAPI.Services.Pacient
{
    public interface IPacientService
    {
        IEnumerable<PacientModel> GetAllPacients();
        PacientModel GetPacientById(int id);
        PacientModel CreatePacient(CreatePacientRequest pacient);
        PacientModel UpdatePacient(int id, PacientModel updatedPacient);
        PacientModel DeletePacient(int id);
        bool PacientExists(int pacientId);
        void PatchPacient(int pacientId, int newReportId);
        ReportModel CreateReport(int pacientId, CreateReportRequest report);
    }
}

using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.PacientModel;

namespace Anamnese.API.Application.Services.Pacient
{
    public interface IPacientService
    {
        IEnumerable<PacientModel> GetAllPacients();
        PacientModel GetPacientById(int id);
        IEnumerable<PacientModel> GetPacientsByProfissional();
        PacientModel CreatePacient(CreatePacientRequest pacient);
        PacientModel UpdatePacient(int id, PacientModel updatedPacient);
        PacientModel DeletePacient(int id);
        bool PacientExists(int pacientId);
        void PatchPacient(int pacientId, int newReportId);
        int CountAllPacients();
        int CountAllProfissionalPacients();
    }
}

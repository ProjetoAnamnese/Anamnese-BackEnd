using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Filters;
using Anamnese.API.ORM.Model.PacientModel;

namespace Anamnese.API.Application.Services.Pacient
{
    public interface IPacientService
    {
        PagedResponse<PacientModel> GetAllPacients(PacientFilter filters);

        PacientModel GetPacientById(int id);
        IEnumerable<PacientModel> GetPacientsByProfissional();
        PacientModel CreatePacient(CreatePacientRequest pacient);              
        PacientModel UpdatePacient(int id, PacientModel updatedPacient);
        PacientModel DeletePacient(int id);
        bool PacientExists(int pacientId);
        void PatchPacient(int pacientId, int newReportId);
        int CountAllPacients();        
        int CountAllProfissionalPacients();
        Dictionary<string, int> CountPacientBySpecialty();
    }
}

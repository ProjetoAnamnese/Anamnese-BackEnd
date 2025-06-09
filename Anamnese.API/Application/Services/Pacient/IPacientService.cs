using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Filters;
using Anamnese.API.ORM.Model.Common;
using Anamnese.API.ORM.Model.PacientModel;

namespace Anamnese.API.Application.Services.Pacient
{
    public interface IPacientService
    {
        PagedResponse<PacientModel> GetAllPacients(PacientFilter filters);
        Result<PacientResponseModel> CreatePacient(CreatePacientRequest pacient);
        Result<PacientModel> UpdatePacient(int pacientId, UpdatePacientRequest updatedPacient);
        Dictionary<string, int> CountPacientsWithAndWithoutReports();

        PacientModel GetPacientById(int id);
        IEnumerable<PacientModel> GetPacientsByProfissional();

        PacientModel DeletePacient(int id);
        bool PacientExists(int pacientId);
        void PatchPacient(int pacientId, int newReportId);
        int CountAllPacients();        
        int CountAllProfissionalPacients();
        Dictionary<string, int> CountPacientBySpecialty();
    }
}

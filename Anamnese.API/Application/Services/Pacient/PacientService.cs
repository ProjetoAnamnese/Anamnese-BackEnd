using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.PacientModel;

namespace Anamnese.API.Application.Services.Pacient
{
    public class PacientService : IPacientService
    {
        public PacientModel CreatePacient(CreatePacientRequest pacient)
        {
            throw new NotImplementedException();
        }

        public PacientModel DeletePacient(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PacientModel> GetAllPacients()
        {
            throw new NotImplementedException();
        }

        public PacientModel GetPacientById(int id)
        {
            throw new NotImplementedException();
        }

        public bool PacientExists(int pacientId)
        {
            throw new NotImplementedException();
        }

        public void PatchPacient(int pacientId, int newReportId)
        {
            throw new NotImplementedException();
        }

        public PacientModel UpdatePacient(int id, PacientModel updatedPacient)
        {
            throw new NotImplementedException();
        }
    }
}

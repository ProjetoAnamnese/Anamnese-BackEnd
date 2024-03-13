using Anamnese.API.Application.Services.Token;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.PacientModel;
using Anamnese.API.ORM.Repository;
using Microsoft.EntityFrameworkCore;

namespace Anamnese.API.Application.Services.Pacient
{
    public class PacientService : IPacientService
    {
        private readonly BaseRepository<PacientModel> _pacientRepository;
        private ITokenService _tokenService { get; }
        public PacientService(BaseRepository<PacientModel> pacientRepository, ITokenService tokenService)
        {            
            _pacientRepository = pacientRepository;
            _tokenService = tokenService;
        }

        public IEnumerable<PacientModel> GetAllPacients()
        {
          return _pacientRepository.GetAll();
        }
        public PacientModel GetPacientById(int id)
        {
            return _pacientRepository.GetById(id);
        }
        public IEnumerable<PacientModel> GetPacientsByProfissionalId(int id)
        {
            int profissionalId = _tokenService.GetUserId();
            return _pacientRepository.GetAll().Where(p => p.ProfissionalId == profissionalId);
        }
        public PacientModel CreatePacient(CreatePacientRequest pacient)
        {
            int profissionalId = _tokenService.GetUserId();
            var res = _pacientRepository.Add(new PacientModel
            {
                Address = pacient.Address,
                Birth = pacient.Birth,
                Email = pacient.Email,
                Phone = pacient.Phone,
                Profession = pacient.Profession,
                Uf = pacient.Uf,
                Username = pacient.Username,
                Gender = pacient.Gender,
                ProfissionalId= profissionalId,
            });
            _pacientRepository.SaveChanges();
            return res;
        }

           public PacientModel UpdatePacient(int id, PacientModel updatedPacient)
        {
            var existingPacient = _pacientRepository.GetById(id);

            if (existingPacient != null)
            {

                existingPacient.Username = updatedPacient.Username;
                existingPacient.Email = updatedPacient.Email;
                existingPacient.Address = updatedPacient.Address;
                existingPacient.Uf = updatedPacient.Uf;
                existingPacient.Phone = updatedPacient.Phone;
                existingPacient.Birth = updatedPacient.Birth;
                existingPacient.Gender = updatedPacient.Gender;

                _pacientRepository.SaveChanges();

                _pacientRepository.Update(existingPacient);

                return existingPacient;
            }

            return null;
        }

        public PacientModel DeletePacient(int id)
        {
            var pacientToDelete = _pacientRepository.GetById(id);
            if(pacientToDelete != null)
            { 
                _pacientRepository.Delete(pacientToDelete);
                _pacientRepository.SaveChanges();
                return pacientToDelete;                        
            }
            return null;

        }

        public void PatchPacient(int pacientId, int newReportId)
        {
            throw new NotImplementedException();
        }

        public bool PacientExists(int pacientId)
        {
            var pacient = _pacientRepository.GetById(pacientId);
            return pacient != null;
        }

     
    }
}

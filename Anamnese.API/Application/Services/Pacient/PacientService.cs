using Anamnese.API.Application.Services.Token;
using Anamnese.API.ORM.Context;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.PacientModel;
using Anamnese.API.ORM.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace Anamnese.API.Application.Services.Pacient
{
    public class PacientService : IPacientService
    {
        private readonly BaseRepository<PacientModel> _pacientRepository;
        private ITokenService _tokenService { get; }
        private readonly AnamneseDbContext _context;

        public PacientService(BaseRepository<PacientModel> pacientRepository, ITokenService tokenService)
        {            
            _pacientRepository = pacientRepository;
            _tokenService = tokenService;
        }

        public IEnumerable<PacientModel> GetAllPacients()
        {
            return _pacientRepository._context.Pacient
                .Include(e => e.Report)                
                .ToList();
        }
        public PacientModel? GetPacientById(int id)
        {
            var pacient = _pacientRepository._context.Pacient
                .Include(p => p.Report)                
                .FirstOrDefault(p => p.PacientId == id);

            return pacient;
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
                ProfissionalId = profissionalId,
                MedicalSpeciality = null,

            }) ;
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

        public IEnumerable<PacientModel> GetPacientsByProfissional()
        {
            var profissionalId = _tokenService.GetUserId();
            return _pacientRepository._context.Pacient.
                Include(e => e.Report).Where(p => p.ProfissionalId == profissionalId);            
        }

        

        public int CountAllPacients()
        {
            return _pacientRepository.Count();
        }

        public int CountAllProfissionalPacients()
        {
            int profissionalId = _tokenService.GetUserId();
            return _pacientRepository.Count(p => p.ProfissionalId == profissionalId);            
        }

        public Dictionary<string, int> CountPacientBySpecialty()
        {
            var pacientsCount = _pacientRepository.GetAll()
                .GroupBy(r => CultureInfo.CurrentCulture.TextInfo.ToTitleCase((r.MedicalSpeciality ?? "Especialidade não informada").ToLower()))
                .ToDictionary(g => g.Key, g => g.Count());

            return pacientsCount;
        }
    }
}

using Anamnese.API.Application.Services.Token;
using Anamnese.API.ORM.Context;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Filters;
using Anamnese.API.ORM.Model.PacientModel;
using Anamnese.API.ORM.Repository;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public PacientService(BaseRepository<PacientModel> pacientRepository, ITokenService tokenService, IMapper mapper)
        {            
            _pacientRepository = pacientRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public PagedResponse<PacientModel> GetAllPacients(PacientFilter filter)
        {
            var profissionalId = _tokenService.GetUserId();

            var query = _pacientRepository._context.Pacient
                .Include(p => p.Report)
                .Where(p => p.ProfissionalId == profissionalId);

            if (!string.IsNullOrWhiteSpace(filter.Username))
                query = query.Where(p => p.Username.ToLower().Contains(filter.Username.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Uf))
                query = query.Where(p => p.Uf.ToLower() == filter.Uf.ToLower());

            if (!string.IsNullOrWhiteSpace(filter.Gender))
                query = query.Where(p => p.Gender.ToLower() == filter.Gender.ToLower());

            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(p => p.Email.ToLower().Contains(filter.Email.ToLower()));

            var totalCount = query.Count();

            var items = query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            return new PagedResponse<PacientModel>
            {
                Items = items,
                TotalCount = totalCount,
                PerPage = filter.PageSize
            };
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
            var newPacient = _mapper.Map<PacientModel>(pacient);
            newPacient.ProfissionalId = profissionalId;
            var res = _pacientRepository.Add(newPacient);            
            _pacientRepository.SaveChanges();
            var mapperd = _mapper.Map<PacientModel>(res);
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
                .GroupBy(r => CultureInfo.CurrentCulture.TextInfo.ToTitleCase((r.MedicalSpeciality ?? "Pacientes não encaminhados").ToLower()))
                .ToDictionary(g => g.Key, g => g.Count());

            return pacientsCount;
        }
    }
}

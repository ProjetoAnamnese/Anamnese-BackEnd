using Anamnese.API.Application.Utilities;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.ProfissionalModel;
using Anamnese.API.ORM.Repository;

namespace Anamnese.API.Application.Services.ProfissionalAvailable
{
    public class ProfissionalAvailableService : IProfissionalAvailableService
    {
        private readonly BaseRepository<ProfissionalModel> _profissionalRepository;
        private readonly BaseRepository<ProfissionalAvailableModel> _profissionalAvailableRepository;
        public ProfissionalAvailableService(BaseRepository<ProfissionalModel> profissionalRepository, BaseRepository<ProfissionalAvailableModel> profissionalAvailableRepository)
        {
            _profissionalRepository = profissionalRepository;
            _profissionalAvailableRepository = profissionalAvailableRepository;
        }

        public List<ProfissionalAvailableModel> GetProfissionalAvailabilities(int profissionalId)
        {
            var profissionalAvailabilities = _profissionalAvailableRepository.GetAll()
                .Where(avail => avail.ProfissionalId == profissionalId)
                .ToList();

            // Traduz os dias da semana para português
            foreach (var availability in profissionalAvailabilities)
            {
                availability.DayOfWeek = DayOfWeekTranslator.Translate(availability.DayOfWeek);
            }

            return profissionalAvailabilities;
        }

        public bool SetProfissionalAvailability(int profissionalId, ProfissionalAvailableRequest availability)
        {
            // Verifica se o ID do profissional é válido
            if (profissionalId <= 0)
            {
                return false;
            }
            // Verifica se a disponibilidade é válida 
            if (availability.StartTime >= availability.EndTime)
            {
                return false;
            }

            var existsProfissional = _profissionalRepository.GetById(profissionalId);

            // Verifica se o profissional existe
            if (existsProfissional == null)
            {
                return false;
            }

            // Verifica se já existe uma disponibilidade para o mesmo dia da semana
            if (existsProfissional.ProfissionalAvailable != null &&
                existsProfissional.ProfissionalAvailable.Any(avail => avail.DayOfWeek == availability.DayOfWeek))
            {
                return false;
            }

            // Se a lista de disponibilidades do profissional for nula, cria uma nova lista
            if (existsProfissional.ProfissionalAvailable == null)
            {
                existsProfissional.ProfissionalAvailable = new List<ProfissionalAvailableModel>();
            }

            // Adiciona a nova disponibilidade à lista
            var newAvailability = new ProfissionalAvailableModel
            {
                DayOfWeek = availability.DayOfWeek,
                StartTime = availability.StartTime,
                EndTime = availability.EndTime,
            };
            existsProfissional.ProfissionalAvailable.Add(newAvailability);

            // Salva as alterações no repositório
            _profissionalRepository.SaveChanges();

            return true; // Disponibilidade configurada com sucesso
        }
        public bool EditProfissionalAvailability(int availabilityId, ProfissionalAvailableUpdate updatedAvailability)
        {
            var availability = _profissionalAvailableRepository.GetById(availabilityId);
            if (availability == null) return false;

            availability.StartTime = updatedAvailability.StartTime;
            availability.EndTime = updatedAvailability.EndTime;
            _profissionalAvailableRepository.Update(availability);
            _profissionalAvailableRepository.SaveChanges();
            return true;
        }

        public List<ProfissionalModel> GetProfissionalBySpeciality(string speciality)
        {
            var profissionais = _profissionalRepository.GetAll()
                .Where(p => p.Speciality == speciality)
                .ToList();
            return profissionais;
        }
        public bool IsProfissionalAvailable(int profissionalId, TimeOnly appointmentTime, DateOnly appointmentDate)
        {

            //var availDayOfWeek = _profissionalAvailableRepository.GetAll().Where(avail =>
            //    avail.ProfissionalId == profissionalId && avail.DayOfWeek == appointmentDate.DayOfWeek.ToString() && avail.StartTime).ToList();
            var availabilities = _profissionalAvailableRepository.GetAll()
               .Where(avail =>
                   avail.ProfissionalId == profissionalId &&
                   avail.DayOfWeek == appointmentDate.DayOfWeek.ToString() &&
                   avail.StartTime <= appointmentTime && avail.EndTime >= appointmentTime)
               .ToList();

            // Verifica se a data da consulta corresponde a algum dia da semana das disponibilidades
            var appointmentDayOfWeek = appointmentDate.DayOfWeek.ToString();
            foreach (var availability in availabilities)
            {
                // Verifica se o dia da semana da disponibilidade corresponde ao dia da semana da consulta
                if (availability.DayOfWeek.Contains(appointmentDayOfWeek))
                {
                    // Verifica se o horário da consulta está dentro do intervalo de disponibilidade
                    if (availability.StartTime <= appointmentTime && availability.EndTime >= appointmentTime)
                    {
                        return true; 
                    }
                }
            }
            return false;
        } 
    }
}

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
            var profissionalAvalabities = _profissionalAvailableRepository.GetAll()
                .Where(avail => avail.ProfissionalId == profissionalId)                
                .ToList();
            return profissionalAvalabities;
        }

        public bool SetProfissionalAvailability(int profissionalId, ProfissionalAvailableRequest availability)
        {

            // Verifica se o ID do profissional é válido

            if (profissionalId < 0 || profissionalId == null) 
            { 
                return false;
            }

            // Verifica se a disponibilidade é válida 
            if (availability.StartTime >= availability.EndTime)
            {
                return false;
            }

            var existsProfissional = _profissionalRepository.GetById(profissionalId);
            // Verifica se o profissional exist
            if (existsProfissional == null) return false;

            // Verifica se já existe uma disponibilidade para o mesmo dia da semana
            if (existsProfissional.ProfissionalAvailable != null && existsProfissional.ProfissionalAvailable.Any(avail => avail.DayOfWeek == availability.DayOfWeek))
                return false;

            if (existsProfissional.ProfissionalAvailable == null)
            
            existsProfissional.ProfissionalAvailable = new List<ProfissionalAvailableModel>();
            var newAvailability = new ProfissionalAvailableModel
            {
                DayOfWeek = availability.DayOfWeek,
                StartTime = availability.StartTime,
                EndTime = availability.EndTime,
            };
            existsProfissional.ProfissionalAvailable.Add(newAvailability);
            _profissionalRepository.SaveChanges();
            //_profissionalRepository.Update(existsProfissional);
            return true;            
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

        //public bool IsProfissionalAvailable(int profissionalId, DateTime appointmentDateTime)
        //{
        //    var profissional = _profissionalRepository.GetById(profissionalId);
        //    if (profissional == null || profissional.ProfissionalAvailable == null)
        //    {
        //        return false;
        //    }

        //    // Pega o dia da semana do compromisso
        //    string appointmentDayOfWeek = appointmentDateTime.DayOfWeek.ToString();

        //    // Verifica se há disponibilidade 
        //    var availabilityForDay = profissional.ProfissionalAvailable.FirstOrDefault(avail => avail.DayOfWeek == appointmentDayOfWeek);

        //    if (availabilityForDay == null)
        //    {
        //        return false;
        //    }

        //    // Converte StartTime e EndTime para TimeSpan
        //    TimeSpan startTime = TimeSpan.Parse(availabilityForDay.StartTime.ToString());
        //    TimeSpan endTime = TimeSpan.Parse(availabilityForDay.EndTime.ToString());
        //    TimeSpan appointmentTime = appointmentDateTime.TimeOfDay;

        //    if (appointmentTime < startTime || appointmentTime >= endTime)
        //    {
        //        return false;
        //    }

        //    // O médico está disponível 
        //    return true;
        //}
    }
}

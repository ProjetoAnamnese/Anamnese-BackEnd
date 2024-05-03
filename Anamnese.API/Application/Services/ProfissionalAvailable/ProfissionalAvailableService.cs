using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.ProfissionalModel;
using Anamnese.API.ORM.Repository;

namespace Anamnese.API.Application.Services.ProfissionalAvailable
{
    public class ProfissionalAvailableService : IProfissionalAvailableService
    {
        private readonly BaseRepository<ProfissionalModel> _profissionalRepository;
        public ProfissionalAvailableService(BaseRepository<ProfissionalModel> profissionalRepository)
        {
            _profissionalRepository = profissionalRepository;            
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
    }
}

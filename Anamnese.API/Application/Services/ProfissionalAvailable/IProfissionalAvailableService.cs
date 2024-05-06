using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.ProfissionalModel;

namespace Anamnese.API.Application.Services.ProfissionalAvailable
{
    public interface IProfissionalAvailableService
    {
        List<ProfissionalAvailableModel> GetProfissionalAvailabilities(int profissionalId);
        bool EditProfissionalAvailability(int availabilityId, ProfissionalAvailableUpdate updatedAvailability);

        bool SetProfissionalAvailability(int profissionalId, ProfissionalAvailableRequest availability);
        bool IsProfissionalAvailable(int profissionalId, TimeOnly appointmentDateTime, DateOnly appointmentDate);


    }
}

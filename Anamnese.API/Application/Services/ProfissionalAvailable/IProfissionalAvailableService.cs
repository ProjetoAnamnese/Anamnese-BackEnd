using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.ProfissionalModel;

namespace Anamnese.API.Application.Services.ProfissionalAvailable
{
    public interface IProfissionalAvailableService
    {
        List<ProfissionalAvailableModel> GetProfissionalAvailabilities(int profissionalId);
        bool SetProfissionalAvailability(int profissionalId, ProfissionalAvailableRequest availability);

    }
}

using Anamnese.API.ORM.Model.ProfissionalModel;

namespace Anamnese.API.Application.Services.ProfissionalAvailable
{
    public interface IProfissionalAvailableService
    {
        bool SetProfissionalAvailability(int profissionalId, ProfissionalAvailableRequest availability);

    }
}

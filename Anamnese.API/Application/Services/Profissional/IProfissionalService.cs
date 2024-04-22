using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.ProfissionalModel;

namespace Anamnese.API.Application.Services.Profissional
{
    public interface IProfissionalService
    {
        bool IsEmailTaken(string email);
        List<ProfissionalModel> GetDoctorsWithPatients();
        ProfissionalModel GetProfissionalById();
        ProfissionalModel CreateProfissional(CreateProfissionalModel createUserModel);
        ProfissionalModel UpdateProfissional(int id ,UpdateProfissionalRequest updatedUser);
        Task<bool> ValidateCredentials(string email, string password);
        Task<ProfissionalModel> GetUserByEmailAsync(string email);
    }
}

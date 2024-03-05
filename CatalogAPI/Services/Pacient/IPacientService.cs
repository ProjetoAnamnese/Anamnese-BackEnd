using AnamneseAPI.Services.Pacient.Models;
using CatalogAPI.Models;

namespace CatalogAPI.Services.Pacient
{
    public interface IPacientService
    {
        IEnumerable<PacientModel> GetAllPacients();
        PacientModel GetPacientById(int id);
        PacientModel CreatePacient(CreatePacientRequest pacient);
        PacientModel UpdatePacient(int id, PacientModel updatedPacient);
        PacientModel DeletePacient(int id);
    }
}

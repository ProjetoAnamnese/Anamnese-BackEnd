using Anamnese.API.ORM.Context;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.PacientModel;

namespace Anamnese.API.ORM.Seeders.PacientSeeder
{
    public static class PacientSeeder
    {
        public static void SeedPacients(this AnamneseDbContext context)
        {
            var pacients = new List<CreatePacientRequest>
            {
                new CreatePacientRequest
                {
                    Username = "João",
                    Email = "joao@example.com",
                    Phone = "123456789",
                    Address = "Rua A, 123",
                    Profession = "Engineer",
                    Uf = "SP",
                    Birth = new DateOnly(1985, 10, 15),
                    Gender = "Male"
                },
                new CreatePacientRequest
                {
                    Username = "Maria",
                    Email = "maria@example.com",
                    Phone = "987654321",
                    Address = "Rua B, 456",
                    Profession = "Doctor",
                    Uf = "RJ",
                    Birth = new DateOnly(1990, 5, 20),
                    Gender = "Female"
                },
                
                new CreatePacientRequest
                {
                    Username = "Carlos",
                    Email = "carlos@example.com",
                    Phone = "111111111",
                    Address = "Rua C, 789",
                    Profession = "Lawyer",
                    Uf = "MG",
                    Birth = new DateOnly(1978, 8, 25),
                    Gender = "Male"
                },
                new CreatePacientRequest
                {
                    Username = "Ana",
                    Email = "ana@example.com",
                    Phone = "222222222",
                    Address = "Rua D, 101",
                    Profession = "Nurse",
                    Uf = "BA",
                    Birth = new DateOnly(1982, 3, 12),
                    Gender = "Female"
                },                
            };

            foreach (var pacientRequest in pacients)
            {
                var pacient = new PacientModel
                {
                    Username = pacientRequest.Username,
                    Email = pacientRequest.Email,
                    Phone = pacientRequest.Phone,
                    Address = pacientRequest.Address,
                    Profession = pacientRequest.Profession,
                    Uf = pacientRequest.Uf,
                    Birth = pacientRequest.Birth,
                    Gender = pacientRequest.Gender
                };
                context.Pacient.Add(pacient);
            }

            context.SaveChanges();
        }
    }
}

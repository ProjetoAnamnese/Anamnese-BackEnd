using Anamnese.API.ORM.Context;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.ProfissionalModel;
using System;
using System.Collections.Generic;

namespace Anamnese.API.ORM.Seeders.ProfissionalSeeder
{
    public static class ProfissionalSeeder
    {
        public static void SeedProfissionais(this AnamneseDbContext context)
        {
            var profissionais = new List<CreateProfissionalModel>
            {
                new CreateProfissionalModel
                {
                    Username = "doutorjose",
                    Email = "jose@example.com",
                    Password = "senha123",
                    Speciality = "Cardiologia"
                },
                new CreateProfissionalModel
                {
                    Username = "doutormarcos",
                    Email = "marcos@example.com",
                    Password = "senha456",
                    Speciality = "Dermatologia"
                },
                
                new CreateProfissionalModel
                {
                    Username = "doutorana",
                    Email = "ana@example.com",
                    Password = "senha789",
                    Speciality = "Pediatria"
                },
                new CreateProfissionalModel
                {
                    Username = "doutorlucas",
                    Email = "lucas@example.com",
                    Password = "senhaabc",
                    Speciality = "Ortopedia"
                },
                
            };

            foreach (var profissionalRequest in profissionais)
            {
                var profissional = new ProfissionalModel
                {
                    Username = profissionalRequest.Username,
                    Email = profissionalRequest.Email,
                    Password = profissionalRequest.Password,
                    Speciality = profissionalRequest.Speciality
                };
                context.Profissional.Add(profissional);
            }

            context.SaveChanges();
        }
    }
}

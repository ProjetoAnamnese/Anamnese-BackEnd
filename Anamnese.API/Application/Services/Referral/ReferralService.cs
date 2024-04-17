using Anamnese.API.Application.Services.Pacient;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.PacientModel;
using Anamnese.API.ORM.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace Anamnese.API.Application.Services.Referral
{
    public class ReferralService : IReferralService
    {
        private readonly BaseRepository<ReferralModel> _referralRepository;
        private readonly IPacientService _pacientRepository;
        public ReferralService(BaseRepository<ReferralModel> referralRepository, IPacientService pacientRepository)
        {
            _referralRepository = referralRepository;
            _pacientRepository = pacientRepository;
        }


        public ReferralModel SendPacientReferral(int pacientId, ReferralRequestModel referralRequest)
        {
            var existsPacient = _pacientRepository.GetPacientById(pacientId);
            if(existsPacient != null)
            {
                var res = _referralRepository.Add(new ReferralModel
                {
                    PacientId = pacientId,
                    PacientName = existsPacient.Username,
                    MedicalSpeciality = referralRequest.MedicalSpeciality,
                    ReferralDate = DateTime.Now,
                });
                _referralRepository.SaveChanges();
                return res;
            }    
            return null;
        }
        public PacientModel GetReferralByPacientId(int pacientId)
        {
            var existPacient = _pacientRepository.GetPacientById(pacientId);
            if(existPacient != null)
            {
                return existPacient;
            }
            return null;
        }
        public Dictionary<string, int> CountReferralsBySpecialty()
        {
            // Agrupa as referências por especialidade médica, deixando maiuscula a primeira letra de cada palavra
            var referralCounts = _referralRepository.GetAll()
                .GroupBy(r => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(r.MedicalSpeciality.ToLower()))
                .ToDictionary(g => g.Key, g => g.Count());

            return referralCounts;
        }
    }
}

using Anamnese.API.ORM.Entity;

namespace Anamnese.API.Application.Services.Referral
{
    public interface IReferralService
    {
        SpecialityModel SendPacientReferral(int pacientId, ReferralRequestModel referralRequest);
        PacientModel GetReferralByPacientId(int pacientId);
        Dictionary<string, int> CountReferralsBySpecialty();

    }
}

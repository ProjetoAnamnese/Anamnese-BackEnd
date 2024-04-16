using System.Text.Json.Serialization;

namespace Anamnese.API.ORM.Entity
{
    public class ReferralRequestModel
    {        
        public int PacientId { get; set; }
        public string PacientName { get; set; }
        public string MedicalSpeciality { get; set; }       
        
    }
}

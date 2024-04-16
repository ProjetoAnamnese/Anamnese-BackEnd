using System.Text.Json.Serialization;

namespace Anamnese.API.ORM.Entity
{
    public class ReferralModel
    {
        public int Id { get; set; }
        public int PacientId { get; set; }
        public string PacientName { get; set; }
        public string MedicalSpeciality { get; set; }
        public DateTime ReferralDate { get; set; }

        //[JsonIgnore]
        public PacientModel Pacient { get; set; }

        
    }
}

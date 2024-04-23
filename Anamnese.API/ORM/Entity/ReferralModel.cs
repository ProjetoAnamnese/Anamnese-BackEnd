using System.Text.Json.Serialization;

namespace Anamnese.API.ORM.Entity
{
    public class SpecialityModel
    {
        public int Id { get; set; }
        public string Speciality { get; set; }        

        [JsonIgnore]
        public ProfissionalModel Profissional { get; set; }        

        
    }
}

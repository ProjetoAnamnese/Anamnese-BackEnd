using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Anamnese.API.ORM.Entity
{
    public class ProfissionalAvailableModel
    {

        [Key]
        public int Id { get; set; }        
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        [ForeignKey("Profissional")]
        public int ProfissionalId { get; set; }
        [JsonIgnore]
        public ProfissionalModel Profisisonal { get; set; }
    }
}

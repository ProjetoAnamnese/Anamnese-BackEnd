using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anamnese.API.ORM.Entity
{
    public class ProfissionalAvailableModel
    {
        [Key]
        public int ProfissionalAvailableId { get; set; }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string DayOfWeek { get; set; }

        [ForeignKey("ProfissionalId")]
        public int ProfissionalId { get; set; }
        public ProfissionalModel Profissional { get; set; }
    }
}

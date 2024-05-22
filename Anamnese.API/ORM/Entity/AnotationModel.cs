using System.ComponentModel.DataAnnotations;

namespace Anamnese.API.ORM.Entity
{
    public class AnotationModel
    {
        [Key]
        public int AnotationId { get; set; }
        public int PacientId { get; set; }
        public int? ProfissionalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Comments { get; set; }
        public PacientModel Pacient { get; set; }

        public ProfissionalModel Profissional { get; set; }
    }
}

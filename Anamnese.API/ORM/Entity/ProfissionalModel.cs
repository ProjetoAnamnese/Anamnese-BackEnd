using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anamnese.API.ORM.Entity
{
    public class ProfissionalModel
    {
        [Key]
        public int ProfissionalId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Speciality { get; set; }

        public List<ProfissionalAvailableModel> ProfissionalAvailable { get; set; }
        public List<AppointmentModel> Appointments { get; set; }

        //[ForeignKey("Speciality")]
        //public string? SpecialityCode { get; set; }
        //public SpecialityModel Speciality { get; set; }
    }
}

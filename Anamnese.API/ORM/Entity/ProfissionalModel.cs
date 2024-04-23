using System.ComponentModel.DataAnnotations;

namespace Anamnese.API.ORM.Entity
{
    public class ProfissionalModel
    {
        [Key]
        public int ProfissionalId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Specialty { get; set; }

        public List<SpecialityModel> Speciality { get; set; }
        public List<ProfessionalAvailable> ProfessionalAvailable { get; set; }
        public List<ScheduledAppointment> Appointments { get; set; }
    }
}

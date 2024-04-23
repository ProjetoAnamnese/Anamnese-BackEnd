using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anamnese.API.ORM.Entity
{
    public class ScheduledAppointment
    {
        public int Id { get; set; }

        [ForeignKey("Pacient")]
        public int PatientId { get; set; } 

        [ForeignKey("Profissional")]
        public int ProfessionalId { get; set; } 

        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }

        // Propriedades de navegação
        public PacientModel Patient { get; set; }
        public ProfissionalModel Professional { get; set; }
    }
}

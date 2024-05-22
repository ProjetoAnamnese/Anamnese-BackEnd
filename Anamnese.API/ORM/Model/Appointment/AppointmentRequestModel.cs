namespace Anamnese.API.ORM.Model.Appointment
{
    public class AppointmentRequestModel
    {
        public int ProfissionalId { get; set; }
        public int PacientId { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
    }
}

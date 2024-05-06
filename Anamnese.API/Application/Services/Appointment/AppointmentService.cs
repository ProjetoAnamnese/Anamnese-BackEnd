
using Anamnese.API.Application.Services.ProfissionalAvailable;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Repository;

namespace Anamnese.API.Application.Services.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IProfissionalAvailableService _profissionalAvailableService;
        private readonly BaseRepository<AppointmentModel> _appointmentRepository;

        public AppointmentService(IProfissionalAvailableService profissionalAvailableService, BaseRepository<AppointmentModel> appointmentRepository)
        {
            _profissionalAvailableService = profissionalAvailableService;
            _appointmentRepository = appointmentRepository;
        }
        

        public bool ScheduleAppointment(int profissionalId, int pacientId, DateOnly appointmentDate, TimeOnly appointmentTime)
        {
            if (profissionalId == null || pacientId == null) return false;
            
            string dayOfWeek = appointmentDate.DayOfWeek.ToString();

            DateTime appointmentDateTime = new DateTime(appointmentDate.Year,
                appointmentDate.Month,
                appointmentDate.Day,
                appointmentTime.Hour,
                appointmentTime.Minute,
                appointmentTime.Second);

            bool isAvailable = _profissionalAvailableService.IsProfissionalAvailable(profissionalId, appointmentTime, appointmentDate);
            if(isAvailable)
            {
                var appointment = new AppointmentModel
                {
                    PacientId = pacientId,
                    ProfissionalId = profissionalId,
                    AppointmentDateTime = appointmentDateTime
                };
                _appointmentRepository.Add(appointment);
                _appointmentRepository.SaveChanges();
                return true;
            }
            else { return false; }
        }
        //private VerifyDayOfMonth()
        //{

        //}
    }
}

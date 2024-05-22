using Anamnese.API.ORM.Entity;

namespace Anamnese.API.Application.Services.Appointment
{
    public interface IAppointmentService
    {
        //bool ScheduleAppointment(int profissionalId, int pacientId, DateTime appointmentDateTime);
        AppointmentModel GetSpecialityByPacient(int pacientId);
        IEnumerable<AppointmentModel> GetAppointmentByProfissional(int profissionalId);
        bool ScheduleAppointment(int profissionalId, int pacientId, DateOnly appointmentDate, TimeOnly appointmentTime);

    }
}

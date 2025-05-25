using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Filters;

namespace Anamnese.API.Application.Services.Appointment
{
    public interface IAppointmentService
    {
        //bool ScheduleAppointment(int profissionalId, int pacientId, DateTime appointmentDateTime);
        PagedResponse<AppointmentModel> GetAppointmentByProfissional(int pageNumber, int pageSize);


        AppointmentModel GetSpecialityByPacient(int pacientId);
        
        bool ScheduleAppointment(int profissionalId, int pacientId, DateOnly appointmentDate, TimeOnly appointmentTime);

    }
}

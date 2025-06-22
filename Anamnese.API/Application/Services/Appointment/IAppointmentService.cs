using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Filters;
using Anamnese.API.ORM.Model.Appointment;
using Anamnese.API.ORM.Model.Common;

namespace Anamnese.API.Application.Services.Appointment
{
    public interface IAppointmentService
    {
        //bool ScheduleAppointment(int profissionalId, int pacientId, DateTime appointmentDateTime);
        PagedResponse<AppointmentModel> GetAppointmentByProfissional(AppointmentFilter filters, int pageNumber, int pageSize);

        Result<AppointmentModel> UpdateAppointment(int appointmentId, UpdateAppointmentModel updateModel);
        AppointmentModel GetSpecialityByPacient(int pacientId);
        List<AppointmentModel> GetNextAppointmentsOfDay();

        bool ScheduleAppointment(int pacientId, DateOnly appointmentDate, TimeOnly appointmentTime);

    }
}

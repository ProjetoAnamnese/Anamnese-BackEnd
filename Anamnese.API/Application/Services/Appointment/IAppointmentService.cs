namespace Anamnese.API.Application.Services.Appointment
{
    public interface IAppointmentService
    {
        //bool ScheduleAppointment(int profissionalId, int pacientId, DateTime appointmentDateTime);
        bool ScheduleAppointment(int profissionalId, int pacientId, DateOnly appointmentDate, TimeOnly appointmentTime);

    }
}

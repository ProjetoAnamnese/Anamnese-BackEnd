
using Anamnese.API.Application.Services.ProfissionalAvailable;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Repository;

namespace Anamnese.API.Application.Services.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IProfissionalAvailableService _profissionalAvailableService;
        private readonly BaseRepository<ProfissionalModel> _profissionalRepository;
        private readonly BaseRepository<AppointmentModel> _appointmentRepository;

        public AppointmentService(IProfissionalAvailableService profissionalAvailableService, BaseRepository<AppointmentModel> appointmentRepository, BaseRepository<ProfissionalModel> profissionalRepository)
        {
            _profissionalAvailableService = profissionalAvailableService;
            _appointmentRepository = appointmentRepository;
            _profissionalRepository = profissionalRepository;
        }


        public bool ScheduleAppointment(int profissionalId, int pacientId, DateOnly appointmentDate, TimeOnly appointmentTime)
        {
            // Verifica se os IDs do profissional e do paciente são válidos
            var profissioal = _profissionalRepository.GetById(profissionalId);
            if (profissionalId <= 0 || pacientId <= 0)
            {
                return false;
            }

            // Verifica se o profissional já tem uma consulta marcada para o mesmo horário

            // Cria um objeto DateTime com a data e hora da consulta
            DateTime appointmentDateTime = new DateTime(appointmentDate.Year,
                                                        appointmentDate.Month,
                                                        appointmentDate.Day,
                                                        appointmentTime.Hour,
                                                        appointmentTime.Minute,
                                                        appointmentTime.Second);

            bool hasConflict = _appointmentRepository.GetAll().Any(appointment => appointment.ProfissionalId == profissionalId && appointment.AppointmentDateTime == appointmentDateTime);

            if (hasConflict)
            {
                return false; 
            }

            // Verifica se o profissional está disponível no horário desejado
            bool isAvailable = _profissionalAvailableService.IsProfissionalAvailable(profissionalId, appointmentTime, appointmentDate);

            if (isAvailable)
            {
                // Agenda a consulta
                var appointment = new AppointmentModel
                {
                    PacientId = pacientId,
                    ProfissionalId = profissionalId,
                    AppointmentDateTime = appointmentDateTime,
                    Speciality = profissioal.Speciality,
                };

                _appointmentRepository.Add(appointment);
                _appointmentRepository.SaveChanges();
                return true; // Consulta agendada com sucesso
            }
            else
            {
                return false; // O profissional não está disponível para o horário da consulta
            }
        }
        //private VerifyDayOfMonth()
        //{

        //}
    }
}

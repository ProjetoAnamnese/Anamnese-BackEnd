
using Anamnese.API.Application.Services.ProfissionalAvailable;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Repository;

namespace Anamnese.API.Application.Services.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IProfissionalAvailableService _profissionalAvailableService;
        private readonly BaseRepository<ProfissionalModel> _profissionalRepository;
        private readonly BaseRepository<PacientModel> _pacientRepository;
        private readonly BaseRepository<AppointmentModel> _appointmentRepository;

        public AppointmentService(IProfissionalAvailableService profissionalAvailableService, BaseRepository<AppointmentModel> appointmentRepository, BaseRepository<ProfissionalModel> profissionalRepository, BaseRepository<PacientModel> pacientRepository)

        {
            _pacientRepository = pacientRepository;
            _profissionalAvailableService = profissionalAvailableService;
            _appointmentRepository = appointmentRepository;
            _profissionalRepository = profissionalRepository;
        }

        public IEnumerable<AppointmentModel> GetAppointmentByProfissional(int profissionalId)
        {            
            var appointments = _appointmentRepository.GetAll()
                                                     .Where(a => a.ProfissionalId == profissionalId)                                                     
                                                     ;          
            return appointments;
        }

        public AppointmentModel GetSpecialityByPacient(int pacientId)
        {
            var appointments = _appointmentRepository.GetAll().Where(appointment => appointment.PacientId == pacientId).FirstOrDefault();
            return appointments;
            


        }

        public bool ScheduleAppointment(int profissionalId, int pacientId, DateOnly appointmentDate, TimeOnly appointmentTime)
        {
            // Verifica se os IDs do profissional e do paciente são válidos
            if (profissionalId <= 0 || pacientId <= 0)
            {
                return false;
            }

            // Obtém o profissional e o cliente correspondentes aos IDs fornecidos
            var profissional = _profissionalRepository.GetById(profissionalId);
            var pacient = _pacientRepository.GetById(pacientId);

            if (profissional == null || pacient == null)
            {
                return false; // Profissional ou paciente não encontrados
            }

            // Verifica se o profissional já tem uma consulta marcada para o mesmo horário
            DateTime appointmentDateTime = new DateTime(appointmentDate.Year, appointmentDate.Month, appointmentDate.Day,
                                                        appointmentTime.Hour, appointmentTime.Minute, appointmentTime.Second);

            bool hasConflict = _appointmentRepository.GetAll().Any(appointment => appointment.ProfissionalId == profissionalId && appointment.AppointmentDateTime == appointmentDateTime);

            if (hasConflict)
            {
                return false; // Conflito de horário
            }

            // Verifica se o profissional está disponível no horário desejado
            bool isAvailable = _profissionalAvailableService.IsProfissionalAvailable(profissionalId, appointmentTime, appointmentDate);

            if (isAvailable)
            {
                // Agenda a consulta
                var appointment = new AppointmentModel
                {
                    PacientId = pacientId,
                    PacientName = pacient.Username,
                    ProfissionalId = profissionalId,
                    AppointmentDateTime = appointmentDateTime,
                    ProfissionalName = profissional.Username,
                    Speciality = profissional.Speciality, // Define a especialidade como a especialidade do profissional
                };

                _appointmentRepository.Add(appointment);
                _appointmentRepository.SaveChanges();

                // Define a especialidade do cliente como a especialidade do profissional
                pacient.MedicalSpeciality = profissional.Speciality;
                _pacientRepository.Update(pacient);
                _pacientRepository.SaveChanges();

                return true; // Consulta agendada com sucesso
            }
            else
            {
                return false; // Profissional não está disponível no horário desejado
            }
        }
    }
}

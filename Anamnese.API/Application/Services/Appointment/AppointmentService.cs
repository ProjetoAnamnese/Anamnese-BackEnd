
using Anamnese.API.Application.Services.ProfissionalAvailable;
using Anamnese.API.Application.Services.Token;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Filters;
using Anamnese.API.ORM.Model.Appointment;
using Anamnese.API.ORM.Model.Common;
using Anamnese.API.ORM.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Anamnese.API.Application.Services.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IProfissionalAvailableService _profissionalAvailableService;
        private readonly BaseRepository<ProfissionalModel> _profissionalRepository;
        private readonly BaseRepository<PacientModel> _pacientRepository;
        private readonly BaseRepository<AppointmentModel> _appointmentRepository;
        private readonly IMapper _mapper;

        private ITokenService _tokenService { get; }


        public AppointmentService(IProfissionalAvailableService profissionalAvailableService, BaseRepository<AppointmentModel> appointmentRepository, BaseRepository<ProfissionalModel> profissionalRepository, BaseRepository<PacientModel> pacientRepository, ITokenService tokenService, IMapper mapper)

        {
            _tokenService = tokenService;
            _pacientRepository = pacientRepository;
            _profissionalAvailableService = profissionalAvailableService;
            _appointmentRepository = appointmentRepository;
            _profissionalRepository = profissionalRepository;
            _mapper = mapper;

        }

        public PagedResponse<AppointmentModel> GetAppointmentByProfissional(AppointmentFilter filters, int pageNumber = 1, int pageSize = 10)
        {
            int profissionalId = _tokenService.GetUserId();

            var query = _appointmentRepository._context.Appointment
            .Where(a =>
                a.ProfissionalId == profissionalId &&
                a.IsCanceled == filters.IsCanceled &&
                (!filters.AppointmentDateTime.HasValue || a.AppointmentDateTime.Date == filters.AppointmentDateTime.Value.Date)
            );




            var totalCount = query.Count();

            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResponse<AppointmentModel>
            {
                Items = items,
                TotalCount = totalCount,
                PerPage = pageSize
            };
        }


        public List<AppointmentModel> GetNextAppointmentsOfDay()
        {
            int profissionalId = _tokenService.GetUserId();

            return _appointmentRepository._context.Appointment
                .Include(a => a.Pacient)
                .Where(a => a.ProfissionalId == profissionalId &&
                            a.AppointmentDateTime > DateTime.Now)
                .OrderBy(a => a.AppointmentDateTime)
                .Take(5)
                .ToList();
        }


        public AppointmentModel GetSpecialityByPacient(int pacientId)
        {
            var appointments = _appointmentRepository.GetAll().Where(appointment => appointment.PacientId == pacientId).FirstOrDefault();
            return appointments;



        }

        public Result<AppointmentModel> UpdateAppointment(int appointmentId, UpdateAppointmentModel updateModel)
        {
            var existAppointment = _appointmentRepository.GetById(appointmentId);

            if (existAppointment == null)
            {
                throw new Exception("Appointment não encontrado.");
            }

            _mapper.Map(updateModel, existAppointment);
            _appointmentRepository.Update(existAppointment);
            _appointmentRepository.SaveChanges();

            return Result<AppointmentModel>.Ok(_mapper.Map<AppointmentModel>(existAppointment));
        }


        public bool ScheduleAppointment(int pacientId, DateOnly appointmentDate, TimeOnly appointmentTime)
        {

            int profissionalId = _tokenService.GetUserId();
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
                    IsCanceled = false
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

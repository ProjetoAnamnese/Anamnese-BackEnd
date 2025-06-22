using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.Appointment;
using AutoMapper;

namespace Anamnese.API.ORM.Mappings.Appointment
{
    public class AppointmentMappingProfile : Profile
    {
        public AppointmentMappingProfile()
        {
            CreateMap<UpdateAppointmentModel, AppointmentModel>()
                .ForMember(dest => dest.AppointmentId, opt => opt.Ignore())
                .ForMember(dest => dest.Pacient, opt => opt.Ignore())
                .ForMember(dest => dest.Profissional, opt => opt.Ignore());             
        }
    }
}

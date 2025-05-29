using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.PacientModel;
using AutoMapper;

public class PacientMappingProfile : Profile
{
    public PacientMappingProfile()
    {
        CreateMap<CreatePacientRequest, PacientModel>()
            .ForMember(dest => dest.PacientId, opt => opt.Ignore())
            .ForMember(dest => dest.Report, opt => opt.Ignore())
            .ForMember(dest => dest.ProfissionalId, opt => opt.Ignore());

        CreateMap<PacientModel, PacientResponseModel>(); 
    }
}

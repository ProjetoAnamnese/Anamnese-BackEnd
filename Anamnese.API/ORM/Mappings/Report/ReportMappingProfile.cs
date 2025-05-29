using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.PacientModel;
using Anamnese.API.ORM.Model.Report;
using AutoMapper;

public class ReportMappingProfile : Profile
{
    public ReportMappingProfile()
    {
        CreateMap<CreateReportRequest, ReportModel>();
        CreateMap<ReportModel, ReportResponseModel>();
        CreateMap<UpdateReportRequest, ReportModel>()
           .ForMember(dest => dest.PacientId, opt => opt.Ignore())
           .ForMember(dest => dest.ReportId, opt => opt.Ignore())
           .ForMember(dest => dest.PacientName, opt => opt.Ignore())
           .ForMember(dest => dest.Pacient, opt => opt.Ignore());        
    }
}

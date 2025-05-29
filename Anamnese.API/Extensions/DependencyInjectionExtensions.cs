using Anamnese.API.Application.Services.Anotation;
using Anamnese.API.Application.Services.Appointment;
using Anamnese.API.Application.Services.Pacient;
using Anamnese.API.Application.Services.Profissional;
using Anamnese.API.Application.Services.ProfissionalAvailable;
using Anamnese.API.Application.Services.Report;
using Anamnese.API.Application.Services.Token;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace Anamnese.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<BaseRepository<ProfissionalModel>>();
            services.AddScoped<BaseRepository<PacientModel>>();
            services.AddScoped<BaseRepository<ReportModel>>();
            services.AddScoped<BaseRepository<AnotationModel>>();
            services.AddScoped<BaseRepository<ProfissionalAvailableModel>>();
            services.AddScoped<BaseRepository<AppointmentModel>>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProfissionalAvailableService, ProfissionalAvailableService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IProfissionalService, ProfissionalService>();
            services.AddScoped<IPacientService, PacientService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAnotationService, AnotationService>();

            return services;
        }
    }
}

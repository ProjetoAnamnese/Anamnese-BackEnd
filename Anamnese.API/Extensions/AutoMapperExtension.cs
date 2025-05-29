using Anamnese.API.ORM.Mappings.Pacients;

namespace Anamnese.API.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAutoMapper(typeof(PacientMappingProfile));
            services.AddAutoMapper(typeof(Program));

            return services;
        }
    }
}

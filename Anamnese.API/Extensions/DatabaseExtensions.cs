using Anamnese.API.ORM.Context;
using Microsoft.EntityFrameworkCore;

namespace Anamnese.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AnamneseDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                var serverVersion = ServerVersion.AutoDetect(connectionString);
                options.UseMySql(connectionString, serverVersion);
            });
            return services;
        }
    }
}

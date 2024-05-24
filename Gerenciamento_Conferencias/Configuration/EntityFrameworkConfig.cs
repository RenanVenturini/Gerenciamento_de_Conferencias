using Gerenciamento_Conferencias.Data;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_Conferencias.Configuration
{
    public static class EntityFrameworkConfig
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GerenciamentoConferenciasContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("GerenciamentoConferenciasConnection"),
                b => b.MigrationsAssembly(typeof(GerenciamentoConferenciasContext).Assembly.FullName))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());
        }
    }
}

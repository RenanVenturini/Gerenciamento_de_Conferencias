using Gerenciamento_Conferencias.Data.Repository;
using Gerenciamento_Conferencias.Data.Repository.Interfaces;
using Gerenciamento_Conferencias.Services;
using Gerenciamento_Conferencias.Services.Interfaces;

namespace Gerenciamento_Conferencias.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IConferenciaRepository, ConferenciaRepository>();
            services.AddScoped<IPalestraRepository, PalestraRepository>();
            services.AddScoped<IConferenciasService, ConferenciasService>();
            services.AddScoped<IPalestraService, PalestraService>();
        }
    }
}

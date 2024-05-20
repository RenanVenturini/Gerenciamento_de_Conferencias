using AutoMapper;
using Gerenciamento_Conferencias.Data.Table;
using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Models.Response;

namespace Gerenciamento_Conferencias.Data.Mappings_Profiles
{
    public class GerenciamentoProfiles : Profile
    {
        public GerenciamentoProfiles()
        {
            CreateMap<AtualizarConferenciaRequest, Conferencia>();
            CreateMap<ConferenciaRequest, Conferencia>();
            CreateMap<AtualizarPalestraRequest, Palestra>();
            CreateMap<PalestraRequest, Palestra>();
            CreateMap<Conferencia, ConferenciaResponse>();
            CreateMap<Palestra, PalestraResponse>();
            CreateMap<Trilha, TrilhaResponse>();
        }
    }
}

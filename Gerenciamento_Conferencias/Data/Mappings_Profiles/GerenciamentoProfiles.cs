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
            CreateMap<AtualizarTrilhaRequest, Trilha>();
            CreateMap<ConferenciaRequest, Conferencia>();
            CreateMap<TrilhaRequest, Trilha>();
            CreateMap<AtualizarPalestraRequest, Palestra>();
            CreateMap<PalestraRequest, Palestra>();
            CreateMap<Conferencia, ConferenciaResponse>();
            CreateMap<Palestra, PalestraResponse>();
            CreateMap<Trilha, TrilhaResponse>()
                .ForMember(
                    dest => dest.InicioNetworkingEvent,
                    opt => opt.MapFrom(src => src.NetworkingEvent.Inicio)
                );
            CreateMap<NetworkingEventRequest, NetworkingEvent>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => "Networking Event")
                );
            CreateMap<AtualizarNetworkingEventRequest, NetworkingEvent>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => "Networking Event")
                );
        }
    }
}

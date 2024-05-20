using AutoMapper;
using Gerenciamento_Conferencias.Data.Repository.Interfaces;
using Gerenciamento_Conferencias.Data.Table;
using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Models.Response;
using Gerenciamento_Conferencias.Services.Interfaces;

namespace Gerenciamento_Conferencias.Services
{
    public class ConferenciasService : IConferenciasService
    {
        private readonly IConferenciaRepository _conferenciaRepository;
        private readonly IPalestraRepository _palestraRepository;
        private readonly IMapper _mapper;

        public ConferenciasService(IConferenciaRepository conferenciaRepository, IMapper mapper, IPalestraRepository palestraRepository)
        {
            _conferenciaRepository = conferenciaRepository;
            _mapper = mapper;
            _palestraRepository = palestraRepository;
        }

        public async Task CriarConferenciaAsync(ConferenciaRequest conferenciaRequest)
        {
            var conferencia = _mapper.Map<Conferencia>(conferenciaRequest);
            await _conferenciaRepository.CriarConferenciaAsync(conferencia);
        }
        public async Task AtualizarConferenciaAsync(AtualizarConferenciaRequest conferenciaRequest)
        {
            var conferencia = _mapper.Map<Conferencia>(conferenciaRequest);
            await _conferenciaRepository.AtualizarConferenciaAsync(conferencia);
        }

        public async Task<IEnumerable<ConferenciaResponse>> ListarConferenciaAsync()
        {
            var conferencia = await _conferenciaRepository.ListarConferenciaAsync();

            return _mapper.Map<IEnumerable<ConferenciaResponse>>(conferencia);
        }
        public async Task<ConferenciaResponse> ObterConferenciaPorIdAsync(int id)
        {
            var conferencia = await _conferenciaRepository.ObterConferenciaPorIdAsync(id);

            var response = _mapper.Map<ConferenciaResponse>(conferencia);

            foreach (var trilha in response.Trilhas)
            {
                var palestras = conferencia.Trilhas
                 .Where(x => x.Id == trilha.Id)
                .SelectMany(x => x.Palestras)
                .Select(x => $"{x.Inicio} {x.Nome} {x.Duracao}min")
                .ToList();

                trilha.Palestras = palestras;
            }

            return response;
        }
        public async Task ExcluirConferenciaAsync(int id)
        {
            var conferencia = await _conferenciaRepository.ObterConferenciaPorIdAsync(id);
            await _conferenciaRepository.ExcluirConferenciaAsync(conferencia);
        }
       
    }
}

using AutoMapper;
using FluentValidation;
using Gerenciamento_Conferencias.Data.Repository.Interfaces;
using Gerenciamento_Conferencias.Data.Table;
using Gerenciamento_Conferencias.Models.Enum;
using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Models.Response;
using Gerenciamento_Conferencias.Services.Interfaces;
using Gerenciamento_Conferencias.Validators;

namespace Gerenciamento_Conferencias.Services
{
    public class ConferenciasService : IConferenciasService
    {
        private readonly IConferenciaRepository _conferenciaRepository;
        private readonly IMapper _mapper;

        public ConferenciasService(IConferenciaRepository conferenciaRepository, IMapper mapper)
        {
            _conferenciaRepository = conferenciaRepository;
            _mapper = mapper;
        }

        public async Task CriarConferenciaAsync(ConferenciaRequest conferenciaRequest)
        {
            var validator = new ConferenciaRequestValidator()
                .Validate(conferenciaRequest);

            if (!validator.IsValid)
                throw new BadHttpRequestException(validator.Errors.FirstOrDefault().ErrorMessage);

            var conferencia = _mapper.Map<Conferencia>(conferenciaRequest);

            await _conferenciaRepository.CriarConferenciaAsync(conferencia);
        }
        public async Task AtualizarConferenciaAsync(AtualizarConferenciaRequest conferenciaRequest)
        {
            var validator = new AtualizarConferenciaRequestValidator()
                .Validate(conferenciaRequest);

            if (!validator.IsValid)
                throw new BadHttpRequestException(validator.Errors.FirstOrDefault().ErrorMessage);

            var conferencia = _mapper.Map<Conferencia>(conferenciaRequest);
            await _conferenciaRepository.AtualizarConferenciaAsync(conferencia);
        }

        public async Task<IEnumerable<ConferenciaResponse>> ListarConferenciaAsync()
        {
            var conferencias = await _conferenciaRepository.ListarConferenciaAsync();

            var conferenciasResponse = _mapper.Map<IEnumerable<ConferenciaResponse>>(conferencias);

            foreach (var response in conferenciasResponse)
            {
                foreach (var trilha in response.Trilhas)
                {
                    var conferencia = conferencias.FirstOrDefault(x => x.Id == response.Id);

                    var palestras = conferencia.Trilhas
                    .Where(x => x.Id == trilha.Id)
                    .SelectMany(x => x.Palestras)
                    .Select(x => $"{x.Inicio} {x.Nome} {x.Duracao}min")
                    .ToList();

                    var horarios = conferencia.Trilhas
                    .Where(x => x.Id == trilha.Id)
                    .SelectMany(x => x.Palestras).ToList();

                    var network = conferencia.Trilhas
                    .Where(x => x.Id == trilha.Id)
                    .Select(x => x.NetworkingEvent)
                    .FirstOrDefault();

                    trilha.Palestras = palestras;
                    trilha.Palestras.Add($"{network.Inicio} {network.Nome}");
                    trilha.HorariosDisponiveis = PalestraService.ObterPalestrasDisponiveis(horarios, trilha.InicioNetworkingEvent);
                }
            }

            return conferenciasResponse;
        }

        public async Task<ConferenciaResponse> ObterConferenciaPorIdAsync(int id)
        {
            var conferencia = await _conferenciaRepository.ObterConferenciaPorIdAsync(id);

            var response = _mapper.Map<ConferenciaResponse>(conferencia);

            if (response == default)
                throw new BadHttpRequestException("Nenhuma conferência com esse id encontrada!");

            foreach (var trilha in response.Trilhas)
            {
                var palestras = conferencia.Trilhas
                .Where(x => x.Id == trilha.Id)
                .SelectMany(x => x.Palestras)
                .Select(x => $"{x.Inicio} {x.Nome} {x.Duracao}min")
                .ToList();

                var horarios = conferencia.Trilhas
                .Where(x => x.Id == trilha.Id)
                .SelectMany(x => x.Palestras).ToList();

                var network = conferencia.Trilhas
                    .Where(x => x.Id == trilha.Id)
                    .Select(x => x.NetworkingEvent)
                    .FirstOrDefault();

                trilha.Palestras = palestras;
                trilha.Palestras.Add($"{network.Inicio} {network.Nome}");
                trilha.HorariosDisponiveis = PalestraService.ObterPalestrasDisponiveis(horarios, trilha.InicioNetworkingEvent);
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

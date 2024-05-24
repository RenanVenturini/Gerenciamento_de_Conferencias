using AutoMapper;
using Azure;
using FluentValidation;
using Gerenciamento_Conferencias.Data.Repository;
using Gerenciamento_Conferencias.Data.Repository.Interfaces;
using Gerenciamento_Conferencias.Data.Table;
using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Models.Response;
using Gerenciamento_Conferencias.Services.Interfaces;
using Gerenciamento_Conferencias.Validators;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_Conferencias.Services
{
    public class TrilhaService : ITrilhaService
    {
        private readonly ITrilhaRepository _trilhaRepository;
        private readonly IMapper _mapper;

        public TrilhaService(ITrilhaRepository trilhaRepository, IMapper mapper)
        {
            _trilhaRepository = trilhaRepository;
            _mapper = mapper;
        }

        public async Task CriarTrilhaAsync(TrilhaRequest trilhaRequest)
        {
            var validator = new TrilhaRequestValidator()
               .Validate(trilhaRequest);

            if (!validator.IsValid)
                throw new BadHttpRequestException(validator.Errors.FirstOrDefault().ErrorMessage);

            var trilha = _mapper.Map<Trilha>(trilhaRequest);
            await _trilhaRepository.CriarTrilhaAsync(trilha);
        }

        public async Task AtualizarTrilhaAsync(AtualizarTrilhaRequest atualizarTrilhaRequest)
        {
            var validator = new AtualizarTrilhaRequestValidator()
               .Validate(atualizarTrilhaRequest);

            if (!validator.IsValid)
                throw new BadHttpRequestException(validator.Errors.FirstOrDefault().ErrorMessage);

            var trilha = _mapper.Map<Trilha>(atualizarTrilhaRequest);
            await _trilhaRepository.AtualizarTrilhaAsync(trilha);
        }

        public async Task<IEnumerable<TrilhaResponse>> ListarTrilhaAsync()
        {
            var trilhas = await _trilhaRepository.ListarTrilhaAsync();

            var response = _mapper.Map<IEnumerable<TrilhaResponse>>(trilhas);

            foreach (var trilha in response)
            {
                var palestras = trilhas
                    .Where(x => x.Id == trilha.Id)
                    .SelectMany(x => x.Palestras)
                    .Select(x => $"{x.Inicio} {x.Nome} {x.Duracao}min")
                    .ToList();

                var horarios = trilhas
                .Where(x => x.Id == trilha.Id)
                .SelectMany(x => x.Palestras).ToList();

                var network = trilhas
                .Where(x => x.Id == trilha.Id)
                .Select(x => x.NetworkingEvent)
                .FirstOrDefault();

                trilha.Palestras = palestras;
                trilha.Palestras.Add($"{network.Inicio} {network.Nome}");
                trilha.HorariosDisponiveis = PalestraService.ObterPalestrasDisponiveis(horarios, trilha.NetworkingEvent.Inicio);
            }

            return response;
        }
          

        public async Task<TrilhaResponse> ObterTrilhaPorId(int id)
        {
            var trilha = await _trilhaRepository.ObterTrilhaPorId(id);

            var response = _mapper.Map<TrilhaResponse>(trilha);

            var palestras = trilha.Palestras
                    .Select(x => $"{x.Inicio} {x.Nome} {x.Duracao}min")
                    .ToList();

            var horarios = trilha.Palestras.ToList();

            var network = trilha.NetworkingEvent;

            response.Palestras = palestras;
            response.Palestras.Add($"{network.Inicio} {network.Nome}");
            response.HorariosDisponiveis = PalestraService.ObterPalestrasDisponiveis(horarios, trilha.NetworkingEvent.Inicio);

            return response;
        }

        public async Task ExcluirTrilhaAsync(int id)
        {
            var trilha = await _trilhaRepository.ObterTrilhaPorId(id);
            await _trilhaRepository.ExcluirTrilhaAsync(trilha);
        }
    }
}

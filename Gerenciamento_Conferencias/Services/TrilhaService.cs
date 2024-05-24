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
            return _mapper.Map<IEnumerable<TrilhaResponse>>(trilhas);
        }
          

        public async Task<TrilhaResponse> ObterTrilhaPorId(int id)
        {
            var trilha = await _trilhaRepository.ObterTrilhaPorId(id);

            return _mapper.Map<TrilhaResponse>(trilha);
        }

        public async Task ExcluirTrilhaAsync(int id)
        {
            var trilha = await _trilhaRepository.ObterTrilhaPorId(id);
            await _trilhaRepository.ExcluirTrilhaAsync(trilha);
        }
    }
}

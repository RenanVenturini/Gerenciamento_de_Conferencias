using AutoMapper;
using Gerenciamento_Conferencias.Data.Repository;
using Gerenciamento_Conferencias.Data.Repository.Interfaces;
using Gerenciamento_Conferencias.Data.Table;
using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Models.Response;
using Gerenciamento_Conferencias.Services.Interfaces;

namespace Gerenciamento_Conferencias.Services
{
    public class PalestraService : IPalestraService
    {
        private readonly IPalestraRepository _palestraRepository;
        private readonly IMapper _mapper;

        public PalestraService(IPalestraRepository palestraRepository, IMapper mapper)
        {
            _palestraRepository = palestraRepository;
            _mapper = mapper;
        }

        public async Task CriarPalestraAsync(PalestraRequest palestraRequest)
        {
            var palestra = _mapper.Map<Palestra>(palestraRequest);
            await _palestraRepository.CriarPalestraAsync(palestra);
        }
        public async Task AtualizarPalestraAsync(AtualizarPalestraRequest palestraRequest)
        {
            var palestra = _mapper.Map<Palestra>(palestraRequest);
            await _palestraRepository.AtualizarPalestraAsync(palestra);
        }

        public async Task<IEnumerable<PalestraResponse>> ListarPalestraAsync(int trilhaId)
        {
            var palestra = await _palestraRepository.ListarPalestraAsync(trilhaId);
            return _mapper.Map<IEnumerable<PalestraResponse>>(palestra);

        }
        public async Task<PalestraResponse> ObterPalestraPorIdAsync(int id)
        {
            var palestra = await _palestraRepository.ObterPalestraPorId(id);

            return _mapper.Map<PalestraResponse>(palestra);
        }
        public async Task ExcluirPalestraAsync(int id)
        {
            var palestra = await _palestraRepository.ObterPalestraPorId(id);
            await _palestraRepository.ExcluirPalestra(palestra);
        }
    }
}

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
            var palestras = await _palestraRepository.ListarPalestraAsync(palestraRequest.TrilhaId);

            var inicioNetworkIvent = await _palestraRepository.ObterNetwork(palestraRequest.TrilhaId);

            var validator = new PalestraRequestValidator(ObterPalestrasDisponiveis(palestras, inicioNetworkIvent))
                .Validate(palestraRequest);

            if (!validator.IsValid)
                throw new BadHttpRequestException(validator.Errors.FirstOrDefault().ErrorMessage);

            var palestra = _mapper.Map<Palestra>(palestraRequest);

            var inicioPalestra = TimeSpan.Parse(palestraRequest.Inicio);
            var fimPalestra = inicioPalestra.Add(TimeSpan.FromMinutes(palestra.Duracao));
            palestra.Sessao = fimPalestra.Hours <= 12 ? Sessao.Matutino.ToString() : Sessao.Vespertino.ToString();

            await _palestraRepository.CriarPalestraAsync(palestra);
        }
        public async Task AtualizarPalestraAsync(AtualizarPalestraRequest palestraRequest)
        {
            var palestras = await _palestraRepository.ListarPalestraAsync(palestraRequest.TrilhaId);

            palestras = palestras.Where(x => x.Id != palestraRequest.Id).ToList();

            var inicioNetworkIvent = await _palestraRepository.ObterNetwork(palestraRequest.TrilhaId);

            var validator = new AtualizarPalestraRequestValidator(ObterPalestrasDisponiveis(palestras, inicioNetworkIvent))
                .Validate(palestraRequest);

            if (!validator.IsValid)
                throw new BadHttpRequestException(validator.Errors.FirstOrDefault().ErrorMessage);

            var palestra = _mapper.Map<Palestra>(palestraRequest);

            var inicioPalestra = TimeSpan.Parse(palestraRequest.Inicio);
            var fimPalestra = inicioPalestra.Add(TimeSpan.FromMinutes(palestra.Duracao));
            palestra.Sessao = fimPalestra.Hours <= 12 ? Sessao.Matutino.ToString() : Sessao.Vespertino.ToString();

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

        public static List<string> ObterPalestrasDisponiveis(List<Palestra> palestras, string inicioNetworkIvent)
        {
            if (!palestras.Any() || palestras == null)
                return new List<string> { "09:00 as 12:00", $"13:00 as {inicioNetworkIvent}" };

            var horariosDisponiveis = new List<string>();

            var inicioMatutino = TimeSpan.Parse("09:00");
            var fimMatutino = TimeSpan.Parse("12:00");
            var inicioVespertino = TimeSpan.Parse("13:00");
            var fimVespertino = TimeSpan.Parse(inicioNetworkIvent);

            var palestrasMatutinas = palestras.Where(p => p.Sessao == Sessao.Matutino.ToString()).ToList();
            var palestrasVespertinas = palestras.Where(p => p.Sessao == Sessao.Vespertino.ToString()).ToList();

            horariosDisponiveis.AddRange(ObterHorariosDisponiveisSessao(palestrasMatutinas, inicioMatutino, fimMatutino));
            horariosDisponiveis.AddRange(ObterHorariosDisponiveisSessao(palestrasVespertinas, inicioVespertino, fimVespertino));

            return horariosDisponiveis;
        }

        private static List<string> ObterHorariosDisponiveisSessao(List<Palestra> palestras, TimeSpan inicioSessao, TimeSpan fimSessao)
        {
            var horariosDisponiveis = new List<string>();

            var palestrasOrdenadas = palestras.OrderBy(p => TimeSpan.Parse(p.Inicio)).ToList();

            var horarioAtual = inicioSessao;

            foreach (var palestra in palestrasOrdenadas)
            {
                var inicioPalestra = TimeSpan.Parse(palestra.Inicio);
                var fimPalestra = inicioPalestra.Add(TimeSpan.FromMinutes(palestra.Duracao));

                if (horarioAtual < inicioPalestra)
                {
                    horariosDisponiveis.Add($"{horarioAtual:hh\\:mm} as {inicioPalestra:hh\\:mm}");
                }

                horarioAtual = fimPalestra;
            }

            if (horarioAtual < fimSessao)
            {
                horariosDisponiveis.Add($"{horarioAtual:hh\\:mm} as {fimSessao:hh\\:mm}");
            }

            return horariosDisponiveis;
        }
    }
}

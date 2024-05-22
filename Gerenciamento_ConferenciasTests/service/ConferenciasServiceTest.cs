using Bogus;
using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Data.Table;
using Gerenciamento_Conferencias.Services;
using AutoMapper;
using Gerenciamento_Conferencias.Data;
using Microsoft.EntityFrameworkCore;
using Gerenciamento_Conferencias.Data.Repository;
using Gerenciamento_Conferencias.Data.Mappings_Profiles;
using Gerenciamento_Conferencias.Models.Enum;

namespace Gerenciamento_ConferenciasTests.service
{
    public class ConferenciasServiceTest
    {
        [Fact]
        public async Task CriarConferenciaAsync()
        {
            //Arrange
            var fakeNetworkingEventRequest = new Faker<NetworkingEventRequest>()
                .RuleFor(fake => fake.Inicio, "16:30")
                .Generate();

            var fakeTrilhas = new Faker<TrilhaRequest>()
                .RuleFor(fake => fake.Nome, "Inteligencia artificial")
                .RuleFor(fake => fake.NetworkingEvent, () => fakeNetworkingEventRequest)
                .Generate(1);

            var fakeConferenciaRequest = new Faker<ConferenciaRequest>()
                .RuleFor(fake => fake.Nome, "Inteligencia artificial")
                .RuleFor(fake => fake.Local, "Centro")
                .RuleFor(fake => fake.Trilhas, () => fakeTrilhas)
                .Generate();

            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockConferenciaRepository = new ConferenciaRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var conferenciasService = new ConferenciasService(mockConferenciaRepository, new Mapper(config));

                //Act
                await conferenciasService.CriarConferenciaAsync(fakeConferenciaRequest);

                var result = await context.Conferencias
                    .Include(x => x.Trilhas)
                    .FirstOrDefaultAsync();

                var trilha = result?.Trilhas.FirstOrDefault();

                //Assert
                Assert.NotNull(result);
                Assert.NotNull(trilha);
                Assert.NotNull(result.Trilhas);
                Assert.Equal(fakeConferenciaRequest.Nome, result.Nome);
                Assert.Equal(fakeConferenciaRequest.Local, result.Local);
                Assert.True(fakeConferenciaRequest.Trilhas.Any());

                Assert.Equal("Inteligencia artificial", trilha.Nome);

                Assert.Equal("Networking Event", trilha.NetworkingEvent.Nome);
                Assert.Equal(fakeNetworkingEventRequest.Inicio, trilha.NetworkingEvent.Inicio);
            }            
        }

        [Fact]
        public async Task AtualizarConferenciaAsync()
        {
            //Arrange
            var fakeNetworkingEventRequest = new Faker<AtualizarNetworkingEventRequest>()
                .RuleFor(fake => fake.Id, 1)
                .RuleFor(fake => fake.Inicio, "17:00")
                .Generate();

            var fakeTrilhas = new Faker<AtualizarTrilhaRequest>()
                .RuleFor(fake => fake.Id, 1)
                .RuleFor(fake => fake.Nome, "Inteligencia artificial na sociedade")
                .RuleFor(fake => fake.NetworkingEvent, () => fakeNetworkingEventRequest)
                .Generate(1);

            var fakeConferenciaRequest = new Faker<AtualizarConferenciaRequest>()
                .RuleFor(fake => fake.Id, 1)
                .RuleFor(fake => fake.Nome, "Inteligencia artificial")
                .RuleFor(fake => fake.Local, "Centro")
                .RuleFor(fake => fake.Trilhas, () => fakeTrilhas)
                .Generate();

            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                context.Conferencias.Add(new Conferencia
                {
                    Id = 1,
                    Nome = "Atualização da inteligencia na sociedade",
                    Local = "Saúde"                    
                });

                context.Trilhas.Add(new Trilha
                {
                    Id = 1,
                    Nome = "Atualização da inteligencia",
                    ConferenciaId = 1
                });

                context.NetworkingEvents.Add(new NetworkingEvent
                {
                    Id = 1,
                    Nome = "Networking Event",
                    Inicio = "16:30",
                    TrilhaId = 1
                });

                await context.SaveChangesAsync();
            }

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockConferenciaRepository = new ConferenciaRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var conferenciasService = new ConferenciasService(mockConferenciaRepository, new Mapper(config));

                //Act
                await conferenciasService.AtualizarConferenciaAsync(fakeConferenciaRequest);

                var result = await context.Conferencias
                    .Include(x => x.Trilhas)
                    .FirstOrDefaultAsync(x => x.Id == 1);

                var trilha = result?.Trilhas.FirstOrDefault();

                //Assert
                Assert.NotNull(result);
                Assert.NotNull(trilha);
                Assert.NotNull(result.Trilhas);
                Assert.Equal(fakeConferenciaRequest.Nome, result.Nome);
                Assert.Equal(fakeConferenciaRequest.Local, result.Local);
                Assert.True(fakeConferenciaRequest.Trilhas.Any());

                Assert.Equal("Inteligencia artificial na sociedade", trilha.Nome);

                Assert.Equal(fakeNetworkingEventRequest.Id, trilha.NetworkingEvent.Id);
                Assert.Equal("Networking Event", trilha.NetworkingEvent.Nome);
                Assert.Equal(fakeNetworkingEventRequest.Inicio, trilha.NetworkingEvent.Inicio);
            }
        }

        [Theory]
        [InlineData(1)]
        public async Task ObterConferenciaPorIdAsync(int id)
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                context.Conferencias.Add(new Conferencia
                {
                    Id = 1,
                    Nome = "Inteligencia Artificial",
                    Local = "Expo Center Norte"
                });

                context.Trilhas.Add(new Trilha
                {
                    Id = 1,
                    Nome = "Futuro da IA",
                    ConferenciaId = 1
                });

                context.NetworkingEvents.Add(new NetworkingEvent
                {
                    Id = 1,
                    Nome = "Networking Event",
                    Inicio = "17:00",
                    TrilhaId = 1
                });

                context.Palestras.Add(new Palestra
                {
                    Id = 1,
                    Nome = "Fundamentos do .NET",
                    Sessao = Sessao.Matutino.ToString(),
                    Duracao = 40,
                    Inicio = "10:00",
                    TrilhaId = 1
                });

                await context.SaveChangesAsync();
            }

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockConferenciaRepository = new ConferenciaRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var conferenciasService = new ConferenciasService(mockConferenciaRepository, new Mapper(config));

                //Act
                var result = await conferenciasService.ObterConferenciaPorIdAsync(id);

                var trilha = result?.Trilhas.FirstOrDefault();

                var palestras = trilha?.Palestras.FirstOrDefault();

                //Assert
                Assert.NotNull(result);
                Assert.NotNull(trilha);
                Assert.NotNull(result.Trilhas);
                Assert.Equal("Inteligencia Artificial", result.Nome);
                Assert.Equal("Expo Center Norte", result.Local);
                Assert.Equal("Futuro da IA", trilha.Nome);
                Assert.Equal("10:00 Fundamentos do .NET 40min", palestras);

                var resultado = new List<string> { "09:00 as 10:00", "10:40 as 12:00", "13:00 as 17:00" };

                for (int i = 0; i < trilha?.HorariosDisponiveis.Count; i++)
                {
                    Assert.Equal(resultado[i], trilha.HorariosDisponiveis[i]);
                }
            }
        }

        [Fact]
        public async Task ListarConferenciaAsync()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                context.Conferencias.Add(new Conferencia
                {
                    Id = 1,
                    Nome = "Inteligencia Artificial",
                    Local = "Expo Center Norte"
                });

                context.Trilhas.Add(new Trilha
                {
                    Id = 1,
                    Nome = "Futuro da IA",
                    ConferenciaId = 1
                });

                context.NetworkingEvents.Add(new NetworkingEvent
                {
                    Id = 1,
                    Nome = "Networking Event",
                    Inicio = "17:00",
                    TrilhaId = 1
                });

                context.Palestras.Add(new Palestra
                {
                    Id = 1,
                    Nome = "Fundamentos do .NET",
                    Sessao = Sessao.Matutino.ToString(),
                    Duracao = 40,
                    Inicio = "10:00",
                    TrilhaId = 1
                });

                await context.SaveChangesAsync();
            }

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockConferenciaRepository = new ConferenciaRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var conferenciasService = new ConferenciasService(mockConferenciaRepository, new Mapper(config));

                //Act
                var conferencias = await conferenciasService.ListarConferenciaAsync();

                var result = conferencias.FirstOrDefault();

                var trilha = result?.Trilhas.FirstOrDefault();

                var palestras = trilha?.Palestras.FirstOrDefault();

                //Assert
                Assert.NotNull(result);
                Assert.NotNull(trilha);
                Assert.NotNull(result.Trilhas);
                Assert.Equal("Inteligencia Artificial", result.Nome);
                Assert.Equal("Expo Center Norte", result.Local);
                Assert.Equal("Futuro da IA", trilha.Nome);
                Assert.Equal("10:00 Fundamentos do .NET 40min", palestras);

                var resultado = new List<string> { "09:00 as 10:00", "10:40 as 12:00", "13:00 as 17:00" };

                for (int i = 0; i < trilha?.HorariosDisponiveis.Count; i++)
                {
                    Assert.Equal(resultado[i], trilha.HorariosDisponiveis[i]);
                }
            }
        }
    }
}

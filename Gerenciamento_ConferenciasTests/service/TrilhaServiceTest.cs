using AutoMapper;
using Bogus;
using Gerenciamento_Conferencias.Data;
using Gerenciamento_Conferencias.Data.Mappings_Profiles;
using Gerenciamento_Conferencias.Data.Repository;
using Gerenciamento_Conferencias.Data.Table;
using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Gerenciamento_ConferenciasTests.service
{
    public class TrilhaServiceTest
    {
        [Fact]
        public async Task CriarTrilhaAsync()
        {
            // Arrange
            var fakeTrilhaRequest = new Faker<TrilhaRequest>()
                .RuleFor(fake => fake.ConferenciaId, 1)
                .RuleFor(fake => fake.Nome, f => "Primeira trilha")
                .RuleFor(fake => fake.NetworkingEvent, f => new NetworkingEventRequest
                {
                    Inicio = "16:00"
                })
                .Generate();

            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockTrilhaRepository = new TrilhaRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var mapper = new Mapper(config);
                var trilhaService = new TrilhaService(mockTrilhaRepository, mapper);

                // Act
                await trilhaService.CriarTrilhaAsync(fakeTrilhaRequest);
                var result = await context.Trilhas
                    .Include(x => x.NetworkingEvent)
                    .FirstOrDefaultAsync();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(fakeTrilhaRequest.Nome, result.Nome);
                Assert.Equal(fakeTrilhaRequest.NetworkingEvent.Inicio, result.NetworkingEvent.Inicio);
            }
        }

        [Fact]
        public async Task AtualizarTrilhaAsync()
        {
            // Arrange
            var fakeAtualizarTrilhaRequest = new Faker<AtualizarTrilhaRequest>()
                .RuleFor(fake => fake.Id, 1)
                .RuleFor(fake => fake.Nome, "Inteligencia Artificial Atualizada")
                .RuleFor(fake => fake.NetworkingEvent, f => new AtualizarNetworkingEventRequest
                {
                    Id = 1,
                    Inicio = "16:40"
                })
                .Generate();

            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                context.Trilhas.Add(new Trilha
                {
                    Id = 1,
                    ConferenciaId = 1,
                    Nome = "Inteligencia Artificial"
                });

                context.NetworkingEvents.Add(new NetworkingEvent
                {
                    Id = 1,
                    TrilhaId = 1,
                    Nome = "Inteligencia Artificial",
                    Inicio = "16:30"
                });

                await context.SaveChangesAsync();
            }

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockTrilhaRepository = new TrilhaRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var mapper = new Mapper(config);
                var trilhaService = new TrilhaService(mockTrilhaRepository, mapper);

                // Act
                await trilhaService.AtualizarTrilhaAsync(fakeAtualizarTrilhaRequest);
                var result = await context.Trilhas
                .Include(x => x.NetworkingEvent)
                .FirstOrDefaultAsync(t => t.Id == 1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(fakeAtualizarTrilhaRequest.Nome, result.Nome);
                Assert.Equal(fakeAtualizarTrilhaRequest.NetworkingEvent.Inicio, result.NetworkingEvent.Inicio);
            }
        }

        [Fact]
        public async Task ListarTrilhaAsync()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                context.Trilhas.Add(new Trilha 
                { 
                    Id = 1, 
                    ConferenciaId = 1, 
                    Nome = "Trilha 1" 
                });

                context.Trilhas.Add(new Trilha
                {
                    Id = 2,
                    ConferenciaId = 1,
                    Nome = "Trilha 2"
                });

                context.NetworkingEvents.Add(new NetworkingEvent
                {
                    Id = 1,
                    TrilhaId = 1,
                    Nome = "Network Event",
                    Inicio = "16:20"
                });

                context.NetworkingEvents.Add(new NetworkingEvent
                {
                    Id = 2,
                    TrilhaId = 2,
                    Nome = "Network Event",
                    Inicio = "16:00"
                });

                await context.SaveChangesAsync();
            }

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockTrilhaRepository = new TrilhaRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var mapper = new Mapper(config);
                var trilhaService = new TrilhaService(mockTrilhaRepository, mapper);

                // Act
                var result = await trilhaService.ListarTrilhaAsync();

                var teste = JsonConvert.SerializeObject(result);

                var trilha1 = result.FirstOrDefault(x => x.Nome == "Trilha 1");
                var trilha2 = result.FirstOrDefault(x => x.Nome == "Trilha 2");

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count());

                Assert.NotNull(trilha1);
                Assert.Equal(1, trilha1.Id);
                Assert.Equal("Trilha 1", trilha1.Nome);
                Assert.Equal(1, trilha1.NetworkingEvent.Id);
                Assert.Equal("16:20", trilha1.NetworkingEvent.Inicio);

                Assert.NotNull(trilha2);
                Assert.Equal(2, trilha2.Id);
                Assert.Equal("Trilha 2", trilha2.Nome);
                Assert.Equal(2, trilha2.NetworkingEvent.Id);
                Assert.Equal("16:00", trilha2.NetworkingEvent.Inicio);
            }
        }

        [Theory]
        [InlineData(2)]
        public async Task ObterTrilhaPorId(int id)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                context.Trilhas.Add(new Trilha
                {
                    Id = 1,
                    ConferenciaId = 1,
                    Nome = "Trilha 1"
                });

                context.Trilhas.Add(new Trilha
                {
                    Id = 2,
                    ConferenciaId = 1,
                    Nome = "Trilha 2"
                });

                context.NetworkingEvents.Add(new NetworkingEvent
                {
                    Id = 1,
                    TrilhaId = 1,
                    Nome = "Network Event",
                    Inicio = "16:20"
                });

                context.NetworkingEvents.Add(new NetworkingEvent
                {
                    Id = 2,
                    TrilhaId = 2,
                    Nome = "Network Event",
                    Inicio = "16:00"
                });

                await context.SaveChangesAsync();
            }

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockTrilhaRepository = new TrilhaRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var mapper = new Mapper(config);
                var trilhaService = new TrilhaService(mockTrilhaRepository, mapper);

                // Act
                var result = await trilhaService.ObterTrilhaPorId(id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Id);
                Assert.Equal("Trilha 2", result.Nome);
                Assert.Equal("16:00", result.NetworkingEvent.Inicio);
            }
        }

        [Theory]
        [InlineData(1)]
        public async Task ExcluirTrilhaAsync(int id)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                context.Trilhas.Add(new Trilha
                {
                    Id = 1,
                    ConferenciaId = 1,
                    Nome = "Trilha 1"
                });

                context.Trilhas.Add(new Trilha
                {
                    Id = 2,
                    ConferenciaId = 1,
                    Nome = "Trilha 2"
                });

                context.NetworkingEvents.Add(new NetworkingEvent
                {
                    Id = 1,
                    TrilhaId = 1,
                    Nome = "Network Event",
                    Inicio = "16:20"
                });

                context.NetworkingEvents.Add(new NetworkingEvent
                {
                    Id = 2,
                    TrilhaId = 2,
                    Nome = "Network Event",
                    Inicio = "16:00"
                });

                await context.SaveChangesAsync();
            }

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockTrilhaRepository = new TrilhaRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var mapper = new Mapper(config);
                var trilhaService = new TrilhaService(mockTrilhaRepository, mapper);

                // Act
                await trilhaService.ExcluirTrilhaAsync(id);

                var trilha = await context.Trilhas.ToListAsync();

                var networking = await context.NetworkingEvents.ToListAsync();

                // Assert
                Assert.Single(trilha);
                Assert.Single(networking);
            }
        }
    }
}

using AutoMapper;
using Bogus;
using Gerenciamento_Conferencias.Data;
using Gerenciamento_Conferencias.Data.Mappings_Profiles;
using Gerenciamento_Conferencias.Data.Repository;
using Gerenciamento_Conferencias.Data.Table;
using Gerenciamento_Conferencias.Models.Enum;
using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace Gerenciamento_ConferenciasTests.Service
{
    public class PalestraServiceTest
    {
        [Fact]
        public async Task CriarPalestraAsync()
        {
            // Arrange
            var fakePalestraRequest = new Faker<PalestraRequest>()
                .RuleFor(fake => fake.TrilhaId, 1)
                .RuleFor(fake => fake.Nome, f => "Palestra sobre IA")
                .RuleFor(fake => fake.Inicio, "11:00")
                .RuleFor(fake => fake.Duracao, "relâmpago")
                .Generate();

            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                context.Conferencias.Add(new Conferencia
                {
                    Id = 1,
                    Nome = "Primeira trilha"
                });

                context.Trilhas.Add(new Trilha
                {
                    ConferenciaId = 1,
                    Id = 1,
                    Nome = "Primeira trilha"
                });

                context.NetworkingEvents.Add(new NetworkingEvent
                {
                    TrilhaId = 1,
                    Id = 1,
                    Nome = "Networking Event",
                    Inicio = "16:00"
                });

                context.Palestras.Add(new Palestra
                {
                    TrilhaId = 1,
                    Id = 1,
                    Nome = "Fundamentos de .net",
                    Inicio = "10:00",
                    Duracao = 60,
                    Sessao = Sessao.Matutino.ToString()
                });

                await context.SaveChangesAsync();
            }

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockPalestraRepository = new PalestraRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var mapper = new Mapper(config);
                var palestraService = new PalestraService(mockPalestraRepository, mapper);

                // Act
                await palestraService.CriarPalestraAsync(fakePalestraRequest);
                var result = await context.Palestras.FirstOrDefaultAsync(x => x.Nome == "Palestra sobre IA");

                // Assert
                Assert.NotNull(result);
                Assert.Equal(fakePalestraRequest.Nome, result.Nome);
                Assert.Equal(fakePalestraRequest.Inicio, result.Inicio);
                Assert.Equal(Convert.ToInt32(fakePalestraRequest.Duracao), result.Duracao);
            }
        }

        [Fact]
        public async Task AtualizarPalestraAsync()
        {
            // Arrange
            var fakeAtualizarPalestraRequest = new Faker<AtualizarPalestraRequest>()
                .RuleFor(fake => fake.Id, 1)
                .RuleFor(fake => fake.TrilhaId, 1)
                .RuleFor(fake => fake.Nome, "Palestra atualizada sobre IA")
                .RuleFor(fake => fake.Inicio, "10:59")
                .RuleFor(fake => fake.Duracao, "relâmpago")
                .Generate();

            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                context.Conferencias.Add(new Conferencia
                {
                    Id = 1,
                    Nome = "Primeira trilha"
                });

                context.Trilhas.Add(new Trilha
                {
                    ConferenciaId = 1,
                    Id = 1,
                    Nome = "Primeira trilha"
                });

                context.NetworkingEvents.Add(new NetworkingEvent
                {
                    TrilhaId = 1,
                    Id = 1,
                    Nome = "Networking Event",
                    Inicio = "16:00"
                });

                context.Palestras.Add(new Palestra
                {
                    TrilhaId = 1,
                    Id = 1,
                    Nome = "Fundamentos de .net",
                    Inicio = "10:00",
                    Duracao = 60,
                    Sessao = Sessao.Matutino.ToString()
                });

                await context.SaveChangesAsync();
            }

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockPalestraRepository = new PalestraRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var mapper = new Mapper(config);
                var palestraService = new PalestraService(mockPalestraRepository, mapper);

                // Act
                await palestraService.AtualizarPalestraAsync(fakeAtualizarPalestraRequest);
                var result = await context.Palestras.FirstOrDefaultAsync(t => t.Id == 1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(fakeAtualizarPalestraRequest.Nome, result.Nome);
                Assert.Equal(fakeAtualizarPalestraRequest.Inicio, result.Inicio);
                Assert.Equal(Convert.ToInt32(fakeAtualizarPalestraRequest.Duracao), result.Duracao);
            }
        }

        [Fact]
        public async Task ListarPalestraAsync()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                context.Palestras.Add(new Palestra
                {
                    Id = 1,
                    TrilhaId = 1,
                    Nome = "Palestra 1",
                    Inicio = "09:00",
                    Duracao = 60
                });

                context.Palestras.Add(new Palestra
                {
                    Id = 2,
                    TrilhaId = 1,
                    Nome = "Palestra 2",
                    Inicio = "10:00",
                    Duracao = 60
                });

                await context.SaveChangesAsync();
            }

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockPalestraRepository = new PalestraRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var mapper = new Mapper(config);
                var palestraService = new PalestraService(mockPalestraRepository, mapper);

                // Act
                var result = await palestraService.ListarPalestraAsync(1);

                var teste = JsonConvert.SerializeObject(result);

                var palestra1 = result.FirstOrDefault(x => x.Nome == "Palestra 1");
                var palestra2 = result.FirstOrDefault(x => x.Nome == "Palestra 2");

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count());

                Assert.NotNull(palestra1);
                Assert.Equal(1, palestra1.Id);
                Assert.Equal("Palestra 1", palestra1.Nome);

                Assert.NotNull(palestra2);
                Assert.Equal(2, palestra2.Id);
                Assert.Equal("Palestra 2", palestra2.Nome);
            }
        }

        [Theory]
        [InlineData(2)]
        public async Task ObterPalestraPorIdAsync(int id)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                context.Palestras.Add(new Palestra
                {
                    Id = 1,
                    TrilhaId = 1,
                    Nome = "Palestra 1",
                    Inicio = "09:00",
                    Duracao = 60
                });

                context.Palestras.Add(new Palestra
                {
                    Id = 2,
                    TrilhaId = 1,
                    Nome = "Palestra 2",
                    Inicio = "10:00",
                    Duracao = 60
                });

                await context.SaveChangesAsync();
            }

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockPalestraRepository = new PalestraRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var mapper = new Mapper(config);
                var palestraService = new PalestraService(mockPalestraRepository, mapper);

                // Act
                var result = await palestraService.ObterPalestraPorIdAsync(id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Id);
                Assert.Equal("Palestra 2", result.Nome);
            }
        }

        [Theory]
        [InlineData(1)]
        public async Task ExcluirPalestraAsync(int id)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciamentoConferenciasContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                context.Palestras.Add(new Palestra
                {
                    Id = 1,
                    TrilhaId = 1,
                    Nome = "Palestra 1",
                    Inicio = "09:00",
                    Duracao = 60
                });

                context.Palestras.Add(new Palestra
                {
                    Id = 2,
                    TrilhaId = 1,
                    Nome = "Palestra 2",
                    Inicio = "10:00",
                    Duracao = 60
                });

                await context.SaveChangesAsync();
            }

            using (var context = new GerenciamentoConferenciasContext(options))
            {
                var mockPalestraRepository = new PalestraRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var mapper = new Mapper(config);
                var palestraService = new PalestraService(mockPalestraRepository, mapper);

                // Act
                await palestraService.ExcluirPalestraAsync(id);

                var palestras = await context.Palestras.ToListAsync();

                // Assert
                Assert.Single(palestras);
                Assert.DoesNotContain(palestras, p => p.Id == id);
            }
        }
    }
}

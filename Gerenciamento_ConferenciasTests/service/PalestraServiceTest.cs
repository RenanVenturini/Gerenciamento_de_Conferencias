using AutoMapper;
using Bogus;
using Gerenciamento_Conferencias.Data.Repository;
using Gerenciamento_Conferencias.Data.Table;
using Gerenciamento_Conferencias.Data;
using Gerenciamento_Conferencias.Models.Enum;
using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gerenciamento_Conferencias.Data.Mappings_Profiles;

namespace Gerenciamento_ConferenciasTests.service
{
    public class PalestraServiceTest
    {
        [Fact]
        public async Task CriarPalestraAsync()
        {
            //Arrange
            var fakePalestraRequest = new Faker<PalestraRequest>()
                .RuleFor(fake => fake.Nome, "Conhecendo IA")
                .RuleFor(fake => fake.Inicio, "11:00")
                .RuleFor(fake => fake.Duracao, "5")
                .RuleFor(fake => fake.TrilhaId, 1)
                .Generate();

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
                var palestraRepository = new PalestraRepository(context);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GerenciamentoProfiles>();
                });

                var palestraService = new PalestraService(palestraRepository, new Mapper(config));

                //Act
                await palestraService.CriarPalestraAsync(fakePalestraRequest);

                var result = await context.Palestras.FirstOrDefaultAsync(x => x.Nome == "Conhecendo IA");

                //Assert
                Assert.NotNull(result);
                Assert.Equal("Conhecendo IA", result.Nome);
                Assert.Equal("11:00", result.Inicio);
                Assert.Equal(5, result.Duracao);
                Assert.Equal(Sessao.Matutino.ToString(), result.Sessao);
            }
        }
    }
}

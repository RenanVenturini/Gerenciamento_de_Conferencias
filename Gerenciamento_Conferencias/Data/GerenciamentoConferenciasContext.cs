using Gerenciamento_Conferencias.Data.Table;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_Conferencias.Data
{
    public class GerenciamentoConferenciasContext : DbContext
    {
        public GerenciamentoConferenciasContext(DbContextOptions<GerenciamentoConferenciasContext> options) : base(options) { }

        public DbSet<Conferencia> Conferencias { get; set; }
        public DbSet<Palestra> Palestras { get; set; }
        public DbSet<Trilha> Trilhas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conferencia>().ToTable("TbConferencia");
            modelBuilder.Entity<Palestra>().ToTable("TbPalestra");
            modelBuilder.Entity<Trilha>().ToTable("TbTrilha");

            // Relacionamento entre Conferencia e Trilha (1:N)
            modelBuilder.Entity<Conferencia>().ToTable("TbConferencia");
            modelBuilder.Entity<Conferencia>()
                .HasMany(c => c.Trilhas)
                .WithOne()
                .HasForeignKey(t => t.ConferenciaId);

            // Relacionamento entre Trilha e Palestra (1:N)
            modelBuilder.Entity<Trilha>()
                .HasMany(t => t.Palestras)
                .WithOne()
                .HasForeignKey(p => p.TrilhaId);

            // Configuração adicional para listas de HorariosDisponiveis (evitar erros de mapeamento)
            modelBuilder.Entity<Trilha>()
                .Ignore(t => t.HorariosDisponiveis);
        }
    }
}

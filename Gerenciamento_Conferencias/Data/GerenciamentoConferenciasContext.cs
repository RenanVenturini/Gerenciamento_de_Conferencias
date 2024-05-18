using Gerenciamento_Conferencias.Data.Table;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_Conferencias.Data
{
    public class GerenciamentoConferenciasContext : DbContext
    {
        public GerenciamentoConferenciasContext(DbContextOptions<GerenciamentoConferenciasContext> options) : base(options) { }

        public DbSet<Conferencia> Conferencias { get; set; }
        public DbSet<Palestra> Palestras { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
        public DbSet<Trilha> Trilhas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trilha>()
                .HasOne(t => t.Conferencia)
                .WithMany(c => c.Trilhas)
                .HasForeignKey(t => t.ConferenciaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Sessao>()
                .HasOne(s => s.Trilha)
                .WithMany(t => t.SessoesMatutinas)
                .HasForeignKey(s => s.TrilhaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Sessao>()
                .HasOne(s => s.Trilha)
                .WithMany(t => t.SessoesVespertinas)
                .HasForeignKey(s => s.TrilhaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestra>()
                .HasOne(p => p.Sessao)
                .WithMany(s => s.Palestras)
                .HasForeignKey(p => p.SessaoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

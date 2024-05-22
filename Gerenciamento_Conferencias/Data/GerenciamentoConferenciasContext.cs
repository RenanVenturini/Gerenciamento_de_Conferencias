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
        public DbSet<NetworkingEvent> NetworkingEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conferencia>().ToTable("TbConferencia");
            modelBuilder.Entity<Palestra>().ToTable("TbPalestra");
            modelBuilder.Entity<Trilha>().ToTable("TbTrilha");
            modelBuilder.Entity<NetworkingEvent>().ToTable("TbNetworkingEvent");

            modelBuilder.Entity<Conferencia>()
                .HasMany(c => c.Trilhas)
                .WithOne()
                .HasForeignKey(t => t.ConferenciaId);

            modelBuilder.Entity<Trilha>()
                .HasMany(t => t.Palestras)
                .WithOne()
                .HasForeignKey(p => p.TrilhaId);

            modelBuilder.Entity<Trilha>()
                .HasOne(t => t.NetworkingEvent)
                .WithOne(ne => ne.Trilha)
                .HasForeignKey<NetworkingEvent>(ne => ne.TrilhaId);
        }
    }
}

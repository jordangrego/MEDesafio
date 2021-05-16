using ME.Desafio.CrossCutting.Config;
using ME.Desafio.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ME.Desafio.Database
{
    public class DesafioDbContext : DbContext
    {
        public DesafioDbContext() { }

        public DesafioDbContext(DbContextOptions<DesafioDbContext> options) : base(options) { }

        public DbSet<PedidoEntity> Pedidos { get; set; }
        public DbSet<ItemEntity> Itens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(DbSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<PedidoEntity>()
                .ToTable("Pedido");

            model.Entity<ItemEntity>()
                .ToTable("ItemPedido");

            model.Entity<ItemEntity>()
                .HasOne<PedidoEntity>(x => x.Pedido)
                .WithMany(x => x.Itens)
                .HasForeignKey(x => x.PedidoId);
        }
    }
}
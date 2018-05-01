using Microsoft.EntityFrameworkCore;
using ondeTem.Data.Types;
using ondeTem.Domain.CategoriaRoot;
using ondeTem.Domain.EstabelecimentoRoot;
using ondeTem.Domain.ProdutoRoot;

namespace ondeTem.Data.Context
{
    public class OndeTemContext : DbContext
    {
        public OndeTemContext(DbContextOptions<OndeTemContext> options)
            :base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EstabelecimentoMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Estabelecimento> Estabelecimentos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
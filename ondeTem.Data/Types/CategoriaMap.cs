using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ondeTem.Domain.CategoriaRoot;

namespace ondeTem.Data.Types
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("categorias");

            builder.Property(i => i.Nome)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired();
            
            builder.Property(i => i.Descricao)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(i => i.DataHoraCadastro)
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasMany(i => i.Produtos)
                .WithOne(i => i.Categoria)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.Stories)
                .WithOne(i => i.Categoria)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
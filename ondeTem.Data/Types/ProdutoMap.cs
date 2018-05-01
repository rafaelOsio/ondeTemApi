using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ondeTem.Domain.ProdutoRoot;

namespace ondeTem.Data.Types
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("produtos");

            builder.Property(i => i.Nome)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(i => i.Descricao)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500);

            builder.Property(i => i.DataCadastro)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Ignore(i => i.ImageHash);

            builder.Property(i => i.CaminhoImage)
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);
        }
    }
}
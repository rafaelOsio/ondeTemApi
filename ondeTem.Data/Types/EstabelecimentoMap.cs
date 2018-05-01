using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ondeTem.Domain.EstabelecimentoRoot;

namespace ondeTem.Data.Types
{
    public class EstabelecimentoMap : IEntityTypeConfiguration<Estabelecimento>
    {
        public void Configure(EntityTypeBuilder<Estabelecimento> builder)
        {
            builder.ToTable("estabelecimentos");

            builder.Property(i => i.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(i => i.Email)
                .IsUnique();
                
            builder.Property(i => i.PasswordHash)
                .HasDefaultValue(null)
                .IsRequired();

            builder.Ignore(i => i.Password);

            builder.Property(i => i.IsAdmin)
                .HasDefaultValue(false);

            builder.Property(i => i.isComplete)
                .HasDefaultValue(false);

            builder.Property(i => i.Nome)
                .HasColumnType("varchar(65)")
                .HasMaxLength(65);

            builder.Property(i => i.Rua)
                .HasColumnType("varchar(65)")
                .HasMaxLength(65);

            builder.Property(i => i.Bairro)
                .HasColumnType("varchar(65)")
                .HasMaxLength(65);

            builder.Property(i => i.Numero)
                .HasColumnType("varchar(10)")
                .HasMaxLength(10);

            builder.Property(i => i.Complemento)
                .HasColumnType("varchar(65)")
                .HasMaxLength(65);

            builder.Property(i => i.TelefonePrincipal)
                .HasColumnType("varchar(14)")
                .HasMaxLength(14);

            builder.Property(i => i.TelefoneSecundario)
                .HasColumnType("varchar(14)")
                .HasMaxLength(14);

            builder.Property(i => i.Desativado)
                .HasColumnType("bool")
                .HasDefaultValue(false);

            builder.Property(i => i.MensagemParaClientes)
                .HasColumnType("varchar(400)")
                .HasMaxLength(400);

            builder.Property(i => i.DataCadastro)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Ignore(i => i.ImageHash);

            builder.Property(i => i.CaminhoImage)
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);

            builder.HasMany(i => i.Produtos)
                .WithOne(i => i.Estabelecimento)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ondeTem.Domain.StoryRoot;

namespace ondeTem.Data.Types
{
    public class StoryMap : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.ToTable("stories");

            builder.Property(i => i.Descricao)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(i => i.CaminhoImage)
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);

            builder.Property(i => i.DataFinalPostagem)
                .IsRequired();
        }
    }
}
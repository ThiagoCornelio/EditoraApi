using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoEditoraApi.Models;

namespace ProjetoEditoraApi.Data.Mappings
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");

            builder.HasKey(l => l.LivroId);

            builder.Property(l => l.Nome)
                .IsRequired();

            builder.Property(l => l.Paginas)
                .IsRequired();

            builder.HasOne(l => l.Categoria)
               .WithMany()
               .HasForeignKey(l => l.CategoriaId)
               .IsRequired()
               .HasConstraintName("FK_Livro_Categoria");

            builder.HasOne(l => l.Autor)
                .WithMany()
                .HasForeignKey(l => l.AutorId)
                .IsRequired()
                .HasConstraintName("FK_Livro_Autor");

            builder.Property(l => l.Preco)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.Property(l => l.PrecoDolar)
                .IsRequired(false)
                .HasPrecision(10, 2);

            builder.Property(l => l.Ptax)
                .IsRequired(false)
                .HasPrecision(10, 4);

            builder.Property(x => x.DataLancamento)
              .IsRequired(false)
              .HasColumnName("DataLancamento")
              .HasColumnType("SMALLDATETIME");

            builder.Property(x => x.Image)
               .IsRequired(false)
               .HasColumnType("NVARCHAR(max)");
        }
    }
}

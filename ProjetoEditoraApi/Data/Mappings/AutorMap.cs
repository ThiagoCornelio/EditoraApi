
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoEditoraApi.Models;

namespace ProjetoEditoraApi.Data.Mappings
{
    public class AutorMap : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autor");

            //Chave Primaria
            builder.HasKey(a => a.AutorId);

            builder.Property(a => a.AutorId)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            //Propriedades
            builder.Property(a => a.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.DataNascimento)
                .IsRequired()
                .HasColumnName("DataNascimento")
                .HasColumnType("SMALLDATETIME");

            builder.Property(a => a.CPF)
                .IsRequired()
                .HasColumnName("CPF")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(14);

            builder.Property(a => a.RG)
                .IsRequired()
                .HasColumnName("RG")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);

            builder
              .HasOne(x => x.Endereco)
              .WithMany()
              .HasForeignKey(x => x.EnderecoId)
              .HasConstraintName("FK_Autor_Endereco");

            builder.HasMany(x => x.Categorias)
                .WithMany(x => x.Autores)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoriaAutor",
                    post => post
                        .HasOne<Categoria>()
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .HasConstraintName("FK_AutorRole_AutorId")
                        .OnDelete(DeleteBehavior.Cascade),
                    tag => tag
                        .HasOne<Autor>()
                        .WithMany()
                        .HasForeignKey("AutorId")
                        .HasConstraintName("FK_AutorCategoria_CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}

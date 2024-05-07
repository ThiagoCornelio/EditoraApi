using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoEditoraApi.Models;

namespace ProjetoEditoraApi.Data.Mappings
{
    public class EditoraMap : IEntityTypeConfiguration<Editora>
    {
        public void Configure(EntityTypeBuilder<Editora> builder)
        {
            builder.ToTable("Editora");
            builder.HasKey(e => e.EditoraId);

            builder.Property(e => e.Nome)
                .IsRequired();

            builder.Property(e => e.CNPJ)
                .IsRequired();

            builder.Property(x => x.DataCriacao)
                .IsRequired()
                .HasColumnName("DataCriacao")
                .HasColumnType("SMALLDATETIME");

            builder
             .HasOne(x => x.Endereco)
             .WithMany()
             .HasConstraintName("FK_Editora_Endereco");
        }
    }
}

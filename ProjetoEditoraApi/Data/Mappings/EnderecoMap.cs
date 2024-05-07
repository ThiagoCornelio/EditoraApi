using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoEditoraApi.Models;

namespace ProjetoEditoraApi.Data.Mappings
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");

            builder.HasKey(e => e.EnderecoId);

            builder.Property(e => e.Logradouro)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(c => c.Numero)
               .IsRequired(false)
               .HasMaxLength(50);

            builder.Property(c => c.Cep)
               .IsRequired()
               .HasMaxLength(9)
               .IsFixedLength();

            builder.Property(c => c.Complemento)
               .HasMaxLength(250);

            builder.Property(c => c.Bairro)
               .IsRequired();

            builder.Property(c => c.Cidade)
                .IsRequired();

            builder.Property(c => c.Estado)
                .IsRequired();
        }
    }
}

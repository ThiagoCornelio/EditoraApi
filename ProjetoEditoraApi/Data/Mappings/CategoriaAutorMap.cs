//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using ProjetoEditoraApi.Models;

//namespace ProjetoEditoraApi.Data.Mappings
//{
//    public class CategoriaAutorMap : IEntityTypeConfiguration<CategoriaAutor>
//    {
//        public void Configure(EntityTypeBuilder<CategoriaAutor> builder)
//        {
//            builder.HasKey(a => a.CategoriaAutorId);

//            builder.HasOne(c => c.Categoria).WithMany().HasForeignKey(ca => ca.CategoriaId);
//            builder.HasOne(c => c.Autor).WithMany().HasForeignKey(ca => ca.AutorId);
//        }
//    }
//}

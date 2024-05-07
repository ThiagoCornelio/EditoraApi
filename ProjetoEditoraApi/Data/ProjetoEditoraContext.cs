using ProjetoEditoraApi.Data.Mappings;
using ProjetoEditoraApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoEditoraApi.Data
{
    public class ProjetoEditoraContext : DbContext
    {
        public ProjetoEditoraContext() { }

        public ProjetoEditoraContext(DbContextOptions<ProjetoEditoraContext> options): base(options){}

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        //public DbSet<CategoriaAutor> CategoriaAutor { get; set; }
        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios{ get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AutorMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            //modelBuilder.ApplyConfiguration(new CategoriaAutorMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new EditoraMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());
            modelBuilder.ApplyConfiguration(new LivroMap());
        }
    }

}
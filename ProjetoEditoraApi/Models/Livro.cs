namespace ProjetoEditoraApi.Models
{
    public class Livro
    {
        public int LivroId { get; set; }
        public string Nome { get; set; } = null!;
        public virtual Categoria Categoria { get; set; } = null!;
        public int CategoriaId { get; set; }
        public virtual Autor Autor { get; set; } = null!;
        public int AutorId { get; set; }
        public int Paginas { get; set; }
        public decimal Preco { get; set; }
        public decimal? PrecoDolar { get; set; }
        public decimal? Ptax { get; set; }
        public DateTime? DataLancamento { get; set; }
        public string Image { get; set; } = null!;
    }
}

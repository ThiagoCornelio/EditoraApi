using ProjetoEditoraApi.ViewModel.Autor;
using ProjetoEditoraApi.ViewModel.Categoria;

namespace ProjetoEditoraApi.ViewModel.Livro;
public class LivroAutorViewModel
{
    public int LivroId { get; set; }
    public int AutorId { get; set; }
    public IEnumerable<LivroViewModel> LivroList { get; set; } = new List<LivroViewModel>();
    public IEnumerable<AutorViewModel> AutorList { get; set; } = new List<AutorViewModel>();
    public IEnumerable<CategoriaViewModel> CategoriaList { get; set; } = new List<CategoriaViewModel>();

}

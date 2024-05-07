using ProjetoEditoraApi.ViewModel.Autor;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEditoraApi.ViewModel.Categoria;
public class CategoriaAutorViewModel

{
    [Key]
    public int CategoriaAutorId { get; set; }

    public IEnumerable<CategoriaViewModel> CategoriaList { get; set; } = new List<CategoriaViewModel>();


    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public int CategoriaId { get; set; }

    public AutorViewModel? Autor { get; set; } 

    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public int AutorId { get; set; }
}

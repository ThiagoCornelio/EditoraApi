using System.ComponentModel.DataAnnotations;

namespace ProjetoEditoraApi.ViewModel.Categoria;
public class CategoriaViewModel
{
    [Key]
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    [StringLength(200, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Nome { get; set; } = null!;
}

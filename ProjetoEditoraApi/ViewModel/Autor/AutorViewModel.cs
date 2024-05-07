using ProjetoEditoraApi.ViewModel.Categoria;
using ProjetoEditoraApi.ViewModel.Endereco;
using System.ComponentModel.DataAnnotations;
using ProjetoEditoraApi.ViewModel.Livro;
using System.ComponentModel;
using ProjetoEditoraApi.Extensions.Attributes;
using System.Text.Json.Serialization;

namespace ProjetoEditoraApi.ViewModel.Autor;
public class AutorViewModel
{
    [Key]
    public int AutorId { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser fornecido Autor")]
    [StringLength(200, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Nome { get; set; } = null!;

    [Cpf(ErrorMessage ="CPF inválido")]
    [Required(ErrorMessage = "O campo {0} precisa ser fornecido.")]
    public string CPF { get; set; } = null!;

    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public string RG { get; set; } = null!;

    [DisplayName("Data de Nascimento")]
    [DateLessThanToday(ErrorMessage = "O campo {0} precisa ser menor que hoje")]
    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public DateTime? DataNascimento { get; set; }

    public string? DataNascimentoTratada => DataNascimento?.ToString("dd/MM/yyyy");

    [DisplayName("Total de Livros")]
    public int? TotalLivros { get; set; }

    public EnderecoViewModel Endereco { get; set; } = null!;

    public int EnderecoId { get; set; }

    [JsonIgnore]
    public IList<CategoriaViewModel> CategoriaList { get; set; } = new List<CategoriaViewModel>();

    [JsonIgnore]
    public IEnumerable<LivroViewModel> LivroList { get; set; } = new List<LivroViewModel>();

    [JsonIgnore]
    [DisplayName("Categoria Favorita")]
    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public int CategoriaId { get; set; }
}

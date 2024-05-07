using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEditoraApi.ViewModel.Endereco;
public class EnderecoViewModel
{
    [Key]
    public int EnderecoId { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    [StringLength(100, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Logradouro { get; set; } = null!;

    [DisplayName("Número")]
    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public int? Numero { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public string? Complemento { get; set; }

    [DisplayName("CEP")]
    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    [StringLength(9, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 8)]
    public string Cep { get; set; } = null!;

    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public string Bairro { get; set; } = null!;

    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public string Cidade { get; set; } = null!;

    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public string Estado { get; set; } = null!;
}

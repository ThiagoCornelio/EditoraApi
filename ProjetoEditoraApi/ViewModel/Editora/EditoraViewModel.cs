using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ProjetoEditoraApi.Extensions.Attributes;
using ProjetoEditoraApi.ViewModel.Endereco;

namespace ProjetoEditoraApi.ViewModel.Editora;
public class EditoraViewModel
{
    [Key]
    public int EditoraId { get; set; }

    [StringLength(200, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public string Nome { get; set; } = null!;

    [Cnpj(ErrorMessage = "CNPJ Inválido")]
    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public string CNPJ { get; set; } = null!;

    [DisplayName("Data Criação")]
    [DateLessThanToday(ErrorMessage = "O campo {0} precisa ser menor que hoje")]
    [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
    public DateTime DataCriacao { get; set; }

    public EnderecoViewModel Endereco { get; set; } = new();


    public int EnderecoId { get; set; }
}

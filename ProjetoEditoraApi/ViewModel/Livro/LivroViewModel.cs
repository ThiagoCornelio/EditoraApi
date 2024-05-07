using ProjetoEditoraApi.Extensions.Attributes;
using ProjetoEditoraApi.ViewModel.Autor;
using ProjetoEditoraApi.ViewModel.Categoria;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEditoraApi.ViewModel.Livro;
public class LivroViewModel
{
    [Key]
    public int LivroId { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    [StringLength(200, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Nome { get; set; } = null!;

    public CategoriaViewModel? Categoria { get; set; }

    [DisplayName("Categoria")]
    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public int CategoriaId { get; set; }

    public string? NomeCategoria { get; set; }

    public AutorViewModel? Autor { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser fornecido")]
    public int AutorId { get; set; }

    public string? NomeAutor { get; set; } = null!;

    [DisplayName("Páginas")]
    public int Paginas { get; set; }

    [DisplayFormat(DataFormatString = "{0,c")]
    [DisplayName("Preço")]
    public decimal Preco { get; set; }

    [DisplayName("Tipo Moeda")]
    public int TipoMoeda { get; set; }

    public decimal? Ptax { get; set; }

    [DisplayFormat(DataFormatString = "{0,c")]
    [DisplayName("Preço em Dólar")]
    public decimal PrecoDolar { get; set; }


    [DateLessThanToday(ErrorMessage = "O campo {0} precisa ser menor que hoje")]
    [DisplayName("Data de Lançamento")]
    public DateTime? DataLancamento { get; set; }

    [Required(ErrorMessage = "Imagem inválida")]
    public string Base64Image { get; set; } = null!;

}

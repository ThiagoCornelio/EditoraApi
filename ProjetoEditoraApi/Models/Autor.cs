namespace ProjetoEditoraApi.Models;

public class Autor
{
    public int AutorId { get; set; }
    public string Nome { get; set; } = null!;
    public DateTime? DataNascimento { get; set; }
    public string CPF { get; set; } = null!;
    public string RG { get; set; } = string.Empty;
    public int EnderecoId { get; set; }
    public virtual Endereco Endereco { get; set; } = null!;
    public IList<Categoria> Categorias { get; set; } = null!;
}

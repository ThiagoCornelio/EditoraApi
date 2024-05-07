namespace ProjetoEditoraApi.Models;

public class Categoria 
{
    public int CategoriaId { get; set; } 
    public string Nome { get; set; } = string.Empty;

    public List<Autor> Autores { get; set; } = null!;
}

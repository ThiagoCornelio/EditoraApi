namespace ProjetoEditoraApi.Models;
public class Endereco
{
    public int EnderecoId { get; set; }
    public string Logradouro { get; set; } = string.Empty;
    public int? Numero { get; set; }
    public string? Complemento { get; set; }
    public string Cep { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
}

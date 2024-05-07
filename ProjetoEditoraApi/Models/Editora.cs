namespace ProjetoEditoraApi.Models
{
    public class Editora
    {
        public int EditoraId { get; set; }
        public string Nome { get; set; } = null!;
        public string CNPJ { get; set; } = null!;
        public DateTime DataCriacao { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; } = null!;
    }
}

namespace ProjetoEditoraApi.ViewModel.Autor
{
    public class ListAutoresViewModel
    {
        public int AutorId { get; set; }
        public string Nome { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public int TotalLivros { get; set; }
    }
}

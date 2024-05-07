namespace ProjetoEditoraApi.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public List<Role> Roles { get; set; } = new();
    }
}

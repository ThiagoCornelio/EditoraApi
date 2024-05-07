using System.ComponentModel.DataAnnotations;

namespace ProjetoEditoraApi.ViewModel.Usuario
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O E-mail é inválido")]
        public string Email { get; set; } = null!;
    }
}

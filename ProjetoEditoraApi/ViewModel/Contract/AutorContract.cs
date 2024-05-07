using Flunt.Extensions.Br.Validations;
using Flunt.Validations;
using ProjetoEditoraApi.ViewModel.Autor;

namespace ProjetoEditoraApi.ViewModel.Contract;
public class AutorContract : Contract<AutorViewModel>
{
    public AutorContract(AutorViewModel autor)
    {
        Requires()
            .IsNotNullOrWhiteSpace(autor.Nome, "Nome", "O campo Nome precisa ser fornecido")
            .IsGreaterThan(autor.Nome, 5, "Nome", "O campo Nome precisa ter mais de 5 caracteres")
            .IsCpf(autor.CPF, "CPF", "CPF inválido")
            .IsLowerThan(autor.DataNascimento ?? DateTime.Now, DateTime.UtcNow, "DataNascimento", "Data de Nascimento Inválida")
            .IsNotNullOrWhiteSpace(autor.RG, "RG", "O campo RG precisa ser fornecido");
    }
}

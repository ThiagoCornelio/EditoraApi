using Flunt.Extensions.Br.Validations;
using Flunt.Validations;
using ProjetoEditoraApi.ViewModel.Editora;

namespace ProjetoEditoraApi.ViewModel.Contract;
public class EditoraContract : Contract<EditoraViewModel>
{
    public EditoraContract(EditoraViewModel editora)
    {
        Requires()
        .IsNotNullOrWhiteSpace(editora.Nome, "Nome", "O campo Nome precisa ser fornecido")
        .IsGreaterThan(editora.Nome, 5, "Nome", "O campo Nome precisa ter mais de 5 caracteres")
        .IsCnpj(editora.CNPJ, "CPNJ", "CNPJ inválido")
        .IsLowerThan(editora.DataCriacao, DateTime.UtcNow, "DataCriacao", "Data de Criação Inválida");
    }
}

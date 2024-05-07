using Flunt.Extensions.Br.Validations;
using Flunt.Validations;
using ProjetoEditoraApi.ViewModel.Endereco;

namespace ProjetoEditoraApi.ViewModel.Contract;
public class EnderecoContract : Contract<EnderecoViewModel>
{
    public EnderecoContract(EnderecoViewModel endereco)
    {
        Requires()
            .IsNotNullOrEmpty(endereco.Logradouro, "Logradouro", "O campo Logradouro precisa ser fornecedio")
            .IsZipCode(endereco.Cep, "CEP", "CEP Inválido")
            .IsNotNullOrEmpty(endereco.Bairro, "Bairro", "O campo Bairro precisa ser fornecedio")
            .IsNotNullOrEmpty(endereco.Cidade, "Cidade", "O campo Cidade precisa ser fornecedio")
            .IsNotNullOrEmpty(endereco.Estado, "Estado", "O campo Estado precisa ser fornecedio");
    }
}

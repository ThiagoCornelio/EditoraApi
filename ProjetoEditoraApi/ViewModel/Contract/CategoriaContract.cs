using Flunt.Validations;
using ProjetoEditoraApi.ViewModel.Categoria;

namespace ProjetoEditoraApi.ViewModel.Contract
{
    public class CategoriaContract : Contract<CategoriaViewModel>
    {
        public CategoriaContract(CategoriaViewModel categoria)
        {
            Requires()
                .IsNotNullOrWhiteSpace(categoria.Nome, "Nome", "O campo Nome precisa ser fornecido")
                .IsGreaterThan(categoria.Nome, 5, "Nome", "O campo Nome precisa ter mais de 5 caracteres")
                .IsLowerThan(categoria.Nome.Length, 200, "O campo precisa ter menos de 200 caracteres");
        }
    }
}

using Flunt.Validations;
using ProjetoEditoraApi.Models;
using ProjetoEditoraApi.ViewModel.Livro;
using System.Diagnostics.CodeAnalysis;

namespace ProjetoEditoraApi.ViewModel.Contract
{
    public class LivroContract : Contract<LivroViewModel>
    {
        public LivroContract(LivroViewModel livro)
        {
            Requires()
                .IsNotNullOrEmpty(livro.Nome, "Nome", "O campo Nome precisa ser fornecido")
                .IsNotNull(livro.Paginas, "Paginas", "O campo Páginas precisa ser fornecido")
                .IsNotNull(livro.CategoriaId, "CategoriaId", "O campo CategoriaId precisa ser fornecido")
                .IsNotNull(livro.AutorId, "AutorId", "O campo AutorId precisa ser fornecido")
                .IsNotNull(livro.TipoMoeda, "TipoMoeda", "O campo AutorId precisa ser fornecido")
                .IsLowerThan(livro.DataLancamento ?? DateTime.Now, DateTime.UtcNow, "DataLancamento", "Data Lançamento Inválida")
                .IsNotNullOrEmpty(livro.Base64Image, "Base64Image", "O campo Image precisa ser fornecido");
        }
    }
}

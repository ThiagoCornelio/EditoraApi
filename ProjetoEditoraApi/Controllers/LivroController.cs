using ProjetoEditoraApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoEditoraApi.Data;
using ProjetoEditoraApi.Extensions;
using ProjetoEditoraApi.Models;
using ProjetoEditoraApi.ViewModel.Livro;
using System.Net;
using System.Text.RegularExpressions;
using ProjetoEditoraApi.Services;

namespace ProjetoEditora.MVC.Controllers;

[ApiController]
[Authorize]
public class LivroController : ControllerBase
{
    private readonly ProjetoEditoraContext _context;
    private readonly PtaxService _ptaxService;
    public LivroController(ProjetoEditoraContext context, PtaxService ptaxService)
    {
        _context = context;
        _ptaxService = ptaxService;
    }

    [HttpGet("v1/livros")]
    public async Task<ActionResult> GetAllAsync()
    {
        try
        {
            var livroDomainList = await _context
                .Livros
                .Include(x => x.Autor)
                .Include(x => x.Categoria)
                .AsNoTracking()
                .ToListAsync();

            if (!livroDomainList.Any())
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            var ptax = await _ptaxService.ConsultarCotacaoDolar();

            var livroVmList = livroDomainList.ConvertAll(dominio => new LivroViewModel()
            {
                LivroId = dominio.LivroId,
                Nome = dominio.Nome,
                Paginas = dominio.Paginas,
                DataLancamento = dominio.DataLancamento,
                Preco = dominio.Preco,
                PrecoDolar = Math.Round((dominio.Preco / ptax), 2),
                Ptax = ptax,
                Base64Image = dominio.Image,
                AutorId = dominio.AutorId,
                NomeAutor = dominio.Autor.Nome,
                NomeCategoria = dominio.Categoria.Nome
            });

            return Ok(new ResultViewModel<List<LivroViewModel>>(livroVmList));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<LivroViewModel>("05XL01 - Falha interna no servidor"));
        }
    }

    [HttpGet("v1/livro/{id:int}")]
    public async Task<ActionResult> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            var livroDomain = await _context
                .Livros
                .Include(x => x.Autor)
                .Include(x => x.Categoria)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LivroId == id);

            if (livroDomain == null)
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            var ptax = await _ptaxService.ConsultarCotacaoDolar();

            var livroVm = new LivroViewModel
            {
                LivroId = livroDomain.LivroId,
                Nome = livroDomain.Nome,
                Paginas = livroDomain.Paginas,
                DataLancamento = livroDomain.DataLancamento,
                Preco = livroDomain.Preco,
                PrecoDolar = Math.Round((livroDomain.Preco / ptax), 2),
                Ptax = ptax,
                Base64Image = livroDomain.Image,
                AutorId = livroDomain.AutorId,
                NomeAutor = livroDomain.Autor.Nome,
                NomeCategoria = livroDomain.Categoria.Nome
            };

            return Ok(new ResultViewModel<LivroViewModel>(livroVm));
        }

        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<LivroViewModel>("05XL02 - Falha interna no servidor"));
        }
    }

    [HttpGet("v1/livros/autor/{autorId:int}")]
    public async Task<ActionResult> GetByAuthor([FromRoute] int autorId)
    {
        try
        {
            var livroDomainList = await _context
                .Livros
                .Include(x => x.Autor)
                .Include(x => x.Categoria)
                .AsNoTracking()
                .Where(x => x.AutorId == autorId)
                .ToListAsync();

            if (!livroDomainList.Any())
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            var ptax = await _ptaxService.ConsultarCotacaoDolar();

            var livroVmList = livroDomainList.ConvertAll(dominio => new LivroViewModel()
            {
                LivroId = dominio.LivroId,
                Nome = dominio.Nome,
                Paginas = dominio.Paginas,
                DataLancamento = dominio.DataLancamento,
                Preco = dominio.Preco,
                PrecoDolar = Math.Round((dominio.Preco / ptax), 2),
                Ptax = ptax,
                Base64Image = dominio.Image,
                AutorId = autorId,
                NomeAutor = dominio.Autor.Nome,
                NomeCategoria = dominio.Categoria.Nome
            });

            return Ok(new ResultViewModel<List<LivroViewModel>>(livroVmList));
        }

        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<LivroViewModel>("05XL03 - Falha interna no servidor"));
        }
    }

    [HttpPost("v1/livro")]
    public async Task<ActionResult> PostAsync([FromBody] LivroViewModel livroVm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        string nomeImagem;

        try
        {
            nomeImagem = AddImage(livroVm.Base64Image);

            var livroDomain = new Livro()
            {
                Nome = livroVm.Nome,
                AutorId = livroVm.AutorId,
                CategoriaId = livroVm.CategoriaId,
                Paginas = livroVm.Paginas,
                DataLancamento = livroVm.DataLancamento,
                Preco = livroVm.Preco,
                Image = nomeImagem
            };

            await _context.Livros.AddAsync(livroDomain);
            await _context.SaveChangesAsync();

            return Created($"v1/livro/{livroDomain.LivroId}", new ResultViewModel<Livro>(livroDomain));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<LivroViewModel>("05XL04 - Falha interna no servidor"));
        }
    }

    [HttpPut("v1/livro/{id:int}")]
    public async Task<ActionResult> PutAsync([FromRoute] int id, [FromBody] LivroViewModel livroVm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        try
        {
            var livroDomain = await _context.Livros.FirstOrDefaultAsync(x => x.LivroId == id);

            if (livroDomain == null)
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            livroDomain.Nome = livroVm.Nome;
            livroDomain.Paginas = livroVm.Paginas;
            livroDomain.Image = UpdateImage(livroDomain.Image, livroVm.Base64Image);
            livroDomain.DataLancamento = livroVm.DataLancamento;
            livroDomain.Preco = livroVm.Preco;

            _context.Livros.Update(livroDomain);
            await _context.SaveChangesAsync();

            return Ok(new ResultViewModel<Livro>(livroDomain));
        }
        catch (DbUpdateException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<LivroViewModel>("05XL05 - Não foi possível alterar o Livro"));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<LivroViewModel>("05XL06 - Falha interna no servidor"));
        }
    }

    [HttpDelete("v1/livro/{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            var livroDomain = await _context.Livros.FirstOrDefaultAsync(x => x.LivroId == id);

            if (livroDomain == null)
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            RemoveImage(livroDomain.Image);
            _context.Livros.Remove(livroDomain);
            await _context.SaveChangesAsync();

            return Ok(new ResultViewModel<string>("Livro excluído!"));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<LivroViewModel>("05XL07 - Falha interna no servidor")); ;
        }
    }

    private static string UpdateImage(string oldUrl, string newUrl)
    {
        RemoveImage(oldUrl);
        return AddImage(newUrl);
    }

    private static void RemoveImage(string url)
    {
        var inicio = url.LastIndexOf("images/") + "images/".Length;
        var fim = url.LastIndexOf(".jpg");

        if (inicio != -1 && fim != -1)
        {
            var nome = url.Substring(inicio, fim - inicio);
            System.IO.File.Delete($"wwwroot/images/{nome}.jpg");
        }
    }

    private static string AddImage(string base64Image)
    {
        var fileName = $"{Guid.NewGuid()}.jpg";
        var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");
        var bytes = Convert.FromBase64String(data);
        System.IO.File.WriteAllBytesAsync($"wwwroot/images/{fileName}", bytes);

        return $"https://localhost:0000/images/{fileName}";
    }
}
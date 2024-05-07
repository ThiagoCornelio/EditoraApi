using ProjetoEditoraApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoEditoraApi.Data;
using ProjetoEditoraApi.Extensions;
using ProjetoEditoraApi.Models;
using ProjetoEditoraApi.ViewModel.Categoria;
using System.Net;

namespace ProjetoEditoraApi.Controllers;

[ApiController]
[Authorize]
public class CategoriaController : ControllerBase
{
    private readonly ProjetoEditoraContext _context;
    public CategoriaController(ProjetoEditoraContext context)
    {
        _context = context;
    }

    [HttpGet("v1/categorias")]
    public async Task<ActionResult> GetAllAsync()
    {
        try
        {
            var categorias = await _context.Categorias.ToListAsync();

            if (!categorias.Any())
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            return Ok(new ResultViewModel<List<Categoria>>(categorias));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<List<CategoriaViewModel>>(error: "05XC01 - Falha interna no servidor."));
        }
    }

    [HttpGet("v1/categoria/{id:int}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (categoria == null)
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            return Ok(new ResultViewModel<Categoria>(categoria));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<List<CategoriaViewModel>>(error: "05XC02 - Falha interna no servidor."));
        }
    }

    [HttpPost("v1/categoria")]
    public async Task<ActionResult> Post([FromBody] CategoriaViewModel categoriaVm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var categoriaDomain = new Categoria() { Nome = categoriaVm.Nome };

        try
        {
            await _context.Categorias.AddAsync(categoriaDomain);
            await _context.SaveChangesAsync();
            return Created($"v1/categoria/{categoriaDomain.CategoriaId}", new ResultViewModel<Categoria>(categoriaDomain));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<List<CategoriaViewModel>>(error: "05XC03 - Falha interna no servidor."));
        }
    }

    [HttpPost("v1/categorias")]
    public async Task<ActionResult> PostList([FromBody] IList<CategoriaViewModel> categoriaVmList)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var categoriaList = ConverterCategorias(categoriaVmList);

        try
        {
            await _context.Categorias.AddRangeAsync(categoriaList);
            await _context.SaveChangesAsync();
            return Created($"v1/categorias", new ResultViewModel<List<Categoria>>(categoriaList.ToList()));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<List<CategoriaViewModel>>(error: "05XC04 - Falha interna no servidor."));
        }
    }

    [HttpPut("v1/categoria/{id:int}")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] CategoriaViewModel categoriaVm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        try
        {
            var categoriaDomain = await _context.Categorias.FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (categoriaDomain == null)
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            categoriaDomain.Nome = categoriaVm.Nome;

            _context.Categorias.Update(categoriaDomain);
            await _context.SaveChangesAsync();

            return Ok(new ResultViewModel<CategoriaViewModel>(categoriaVm));
        }
        catch (DbUpdateException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<CategoriaViewModel>("05XC05 - Não foi possível alterar a Categoria"));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<CategoriaViewModel>("05XC06 - Falha interna no servidor"));
        }
    }

    [HttpDelete("v1/categoria/{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var categoriaDomain = _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);

            if (categoriaDomain == null)
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            _context.Remove(categoriaDomain);
            await _context.SaveChangesAsync();
            return Ok(new ResultViewModel<string>("Categoria removida", new List<string>()));
        }
        catch (DbUpdateException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<CategoriaViewModel>("05XC07 - Não foi possível excluir a categoria"));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<CategoriaViewModel>("05XC08 - Falha interna no servidor"));
        }
    }

    private static IList<Categoria> ConverterCategorias(IList<CategoriaViewModel> categoriasViewModel)
        => categoriasViewModel.ToList().ConvertAll(x => new Categoria { Nome = x.Nome });
}

using ProjetoEditoraApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoEditoraApi.Data;
using ProjetoEditoraApi.Extensions;
using ProjetoEditoraApi.Models;
using ProjetoEditoraApi.ViewModel.Editora;
using System.Net;
using ProjetoEditoraApi.ViewModel.Autor;
using ProjetoEditoraApi.ViewModel.Endereco;

namespace ProjetoEditoraApi.Controllers;

[ApiController]
[Authorize]
public class EditoraController : ControllerBase
{
    private readonly ProjetoEditoraContext _context;

    public EditoraController(ProjetoEditoraContext context)
    {
        _context = context;
    }

    [HttpGet("v1/editoras")]
    public async Task<ActionResult> GetAllAsync()
    {
        try
        {
            var editoraList = await _context
                .Editoras
                .Include(x=>x.Endereco)
                .Select(x => new EditoraViewModel
                {
                    EditoraId = x.EditoraId,
                    Nome = x.Nome,
                    CNPJ = x.CNPJ,
                    DataCriacao = x.DataCriacao,
                    Endereco = new EnderecoViewModel()
                    {
                        Bairro = x.Endereco.Bairro,
                        Cep = x.Endereco.Cep,
                        Cidade = x.Endereco.Cidade,
                        Complemento = x.Endereco.Complemento,
                        EnderecoId = x.EnderecoId,
                        Estado = x.Endereco.Estado,
                        Logradouro = x.Endereco.Logradouro,
                        Numero = x.Endereco.Numero
                    }
                })
                .AsNoTracking()
                .ToListAsync();

            if (!editoraList.Any())
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            return Ok(new ResultViewModel<List<EditoraViewModel>>(editoraList));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<EditoraViewModel>("05XE01 - Falha interna no servidor"));
        }
    }

    [HttpGet("v1/editora/{id:int}")]
    public async Task<ActionResult> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            var editoraDomain = await _context
               .Editoras
               .Include(x => x.Endereco)
               .Select(x => new EditoraViewModel
               {
                   EditoraId = x.EditoraId,
                   Nome = x.Nome,
                   CNPJ = x.CNPJ,
                   DataCriacao = x.DataCriacao,
                   Endereco = new EnderecoViewModel()
                   {
                       Bairro = x.Endereco.Bairro,
                       Cep = x.Endereco.Cep,
                       Cidade = x.Endereco.Cidade,
                       Complemento = x.Endereco.Complemento,
                       EnderecoId = x.EnderecoId,
                       Estado = x.Endereco.Estado,
                       Logradouro = x.Endereco.Logradouro,
                       Numero = x.Endereco.Numero
                   }
               })
               .AsNoTracking()
               .FirstOrDefaultAsync(e => e.EditoraId == id);

            if (editoraDomain == null)
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            return Ok(new ResultViewModel<EditoraViewModel>(editoraDomain));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<EditoraViewModel>("05XE02 - Falha interna no servidor"));
        }
    }

    [HttpPost("v1/editora")]
    public async Task<ActionResult> PostAsync([FromBody] EditoraViewModel editoraVm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var editora = new Editora()
        {
            CNPJ = editoraVm.CNPJ,
            DataCriacao = editoraVm.DataCriacao,
            Nome = editoraVm.Nome,
            Endereco = new Endereco()
            {
                Logradouro = editoraVm.Endereco.Logradouro,
                Numero = editoraVm.Endereco.Numero,
                Complemento = editoraVm.Endereco.Complemento ?? string.Empty,
                Cep = editoraVm.Endereco.Cep,
                Bairro = editoraVm.Endereco.Bairro,
                Cidade = editoraVm.Endereco.Cidade,
                Estado = editoraVm.Endereco.Estado
            }
        };

        try
        {
            await _context.Editoras.AddAsync(editora);
            await _context.SaveChangesAsync();

            return Ok(new ResultViewModel<Editora>(editora));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<List<EditoraViewModel>>(error: "05XE03 - Falha interna no servidor"));
        }
    }

    [HttpPut("v1/editora/{id:int}")]
    public async Task<ActionResult> PutAsync([FromRoute] int id, [FromBody] EditoraViewModel editoraVm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        try
        {
            var editoraDomain = await _context
                .Editoras
                .Include(x => x.Endereco)
                .FirstOrDefaultAsync(x => x.EditoraId == id);

            if (editoraDomain == null)
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            editoraDomain.Nome = editoraVm.Nome;
            editoraDomain.DataCriacao = editoraVm.DataCriacao;
            editoraDomain.CNPJ = editoraVm.CNPJ.ToString();

            editoraDomain.Endereco.Logradouro = editoraVm.Endereco.Logradouro;
            editoraDomain.Endereco.Numero = editoraVm.Endereco.Numero;
            editoraDomain.Endereco.Complemento = editoraVm.Endereco.Complemento;
            editoraDomain.Endereco.Cep = editoraVm.Endereco.Cep;
            editoraDomain.Endereco.Bairro = editoraVm.Endereco.Bairro;
            editoraDomain.Endereco.Cidade = editoraVm.Endereco.Cidade;
            editoraDomain.Endereco.Estado = editoraVm.Endereco.Estado;

            _context.Editoras.Update(editoraDomain);
            await _context.SaveChangesAsync();

            return Ok(new ResultViewModel<EditoraViewModel>(editoraVm));
        }
        catch (DbUpdateException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<EditoraViewModel>("05XE04 - Não foi possível alterar o Editora"));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<EditoraViewModel>("05XE05 - Falha interna no servidor"));
        }
    }

    [HttpDelete("v1/editora/{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var editoraDomain = await _context
                .Editoras
                .Include(e => e.Endereco)
                .FirstOrDefaultAsync(x => x.EditoraId == id);

            if (editoraDomain == null)
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            _context.Enderecos.Remove(editoraDomain.Endereco);
            _context.Editoras.Remove(editoraDomain);
            await _context.SaveChangesAsync();

            return Ok(new ResultViewModel<string>("Editora removida!", new List<string>()));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<List<EditoraViewModel>>(error: "05XE06 - Falha interna no servidor"));
        }
    }
}
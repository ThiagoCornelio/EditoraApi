using ProjetoEditoraApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoEditoraApi.Data;
using ProjetoEditoraApi.Extensions;
using ProjetoEditoraApi.Models;
using ProjetoEditoraApi.ViewModel.Autor;
using System.Net;
using ProjetoEditoraApi.ViewModel.Endereco;

namespace ProjetoEditoraApi.Controllers;
[ApiController]
[Authorize]
public class AutorController : ControllerBase
{
    private readonly ProjetoEditoraContext _context;

    public AutorController(ProjetoEditoraContext context)
    {
        _context = context;
    }

    [HttpGet("v1/autores")]
    public async Task<ActionResult> GetAllAsync()
    {
        try
        {
            var autorList = await _context
                .Autores
                .Include(x => x.Endereco)
                .Select(x => new AutorViewModel
                {
                    AutorId = x.AutorId,
                    Nome = x.Nome,
                    CPF = x.CPF,
                    RG = x.RG,
                    DataNascimento = x.DataNascimento,
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

            if (!autorList.Any())
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            return Ok(new ResultViewModel<List<AutorViewModel>>(autorList));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<AutorViewModel>>("05XA01 - Falha interna no servidor"));
        }
    }

    [HttpGet("v1/autor/{id:int}")]
    public async Task<ActionResult> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            var autorDomain = await _context
                .Autores
                .Include(x => x.Endereco)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AutorId == id);

            if (autorDomain == null)
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            return Ok(new ResultViewModel<Autor>(autorDomain));
        }

        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<AutorViewModel>("05XA02 - Falha interna no servidor"));
        }
    }

    [HttpPost("v1/autor")]
    public async Task<ActionResult> PostAsync([FromBody] AutorViewModel autorVm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var autor = new Autor()
        {
            CPF = autorVm.CPF,
            DataNascimento = autorVm.DataNascimento,
            Nome = autorVm.Nome,
            RG = autorVm.RG,
            Endereco = new Endereco()
            {
                Logradouro = autorVm.Endereco.Logradouro,
                Numero = autorVm.Endereco.Numero,
                Complemento = autorVm.Endereco.Complemento,
                Cep = autorVm.Endereco.Cep,
                Bairro = autorVm.Endereco.Bairro,
                Cidade = autorVm.Endereco.Cidade,
                Estado = autorVm.Endereco.Estado
            }
        };

        try
        {
            await _context.Autores.AddAsync(autor);
            await _context.SaveChangesAsync();
            return Created($"v1/autor/{autor.AutorId}", new ResultViewModel<Autor>(autor));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<List<AutorViewModel>>(error: "05XA03 - Falha interna no servidor."));
        }
    }

    [HttpPut("v1/autor/{id:int}")]
    public async Task<ActionResult> PutAsync([FromRoute] int id, [FromBody] AutorViewModel autorVm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        try
        {
            var autorDomain = await _context
                .Autores
                .Include(x => x.Endereco)
                .FirstOrDefaultAsync(x => x.AutorId == id);

            if (autorDomain == null)
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            autorDomain.Nome = autorVm.Nome;
            autorDomain.DataNascimento = autorVm.DataNascimento;
            autorDomain.CPF = autorVm.CPF;
            autorDomain.RG = autorVm.RG;

            if (autorDomain.Endereco != null)
            {
                autorDomain.Endereco.Logradouro = autorVm.Endereco.Logradouro;
                autorDomain.Endereco.Numero = autorVm.Endereco.Numero;
                autorDomain.Endereco.Complemento = autorVm.Endereco.Complemento;
                autorDomain.Endereco.Cep = autorVm.Endereco.Cep;
                autorDomain.Endereco.Bairro = autorVm.Endereco.Bairro;
                autorDomain.Endereco.Cidade = autorVm.Endereco.Cidade;
                autorDomain.Endereco.Estado = autorVm.Endereco.Estado;
            }

            _context.Autores.Update(autorDomain);
            await _context.SaveChangesAsync();

            return Ok(new ResultViewModel<AutorViewModel>(autorVm));
        }
        catch (DbUpdateException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<AutorViewModel>("05XA04 - Não foi possível alterar o Autor"));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<AutorViewModel>("05XA05 - Falha interna no servidor"));
        }
    }

    [HttpDelete("v1/autor/{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var autorDomain = await _context.Autores
                .Include(x => x.Endereco)
                .FirstOrDefaultAsync(x => x.AutorId == id);

            if (autorDomain == null)
                return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

            _context.Autores.Remove(autorDomain);
            await _context.SaveChangesAsync();

            return Ok(new ResultViewModel<string>("Autor removido com sucesso", new List<string>()));
        }
        catch (DbUpdateException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<AutorViewModel>("05XA06 - Não foi possível excluir o autor"));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<AutorViewModel>("05XA07 - Falha interna no servidor"));
        }
    }
}
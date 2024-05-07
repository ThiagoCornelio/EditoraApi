using ProjetoEditoraApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoEditoraApi.Data;
using ProjetoEditoraApi.Extensions;
using ProjetoEditoraApi.Models;
using ProjetoEditoraApi.Services;
using ProjetoEditoraApi.ViewModel.Usuario;
using SecureIdentity.Password;
using System.Net;

namespace ProjetoEditoraApi.Controllers;

[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly ProjetoEditoraContext _context;
    private readonly TokenService _tokenService;
    public UsuarioController(ProjetoEditoraContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("v1/usuarios/")]
    public async Task<IActionResult> Post([FromBody] RegisterViewModel model/*,[FromServices] EmailService emailService*/)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var usuario = new Usuario
        {
            Nome = model.Nome,
            Email = model.Email,
            Slug = model.Email.Replace("@", "-").Replace(".", "-")
        };

        var password = PasswordGenerator.Generate(25);
        usuario.PasswordHash = PasswordHasher.Hash(password);

        try
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            //emailService.Send(user.Name, user.Email, "Bem vindo!", $"Sua senha é {password}");
            return Ok(new ResultViewModel<LoginViewModel>(new LoginViewModel()
            {
                Login = usuario.Email,
                Password = password
            }));
        }
        catch (DbUpdateException)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, new ResultViewModel<string>("05X99 - Este E-mail já está cadastrado"));
        }
        catch
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<string>("05X04 - Falha interna no servidor"));
        }
    }

    [HttpPost("v1/usuario/login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        try
        {
            var user = await _context
                .Usuarios
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
                return StatusCode((int)HttpStatusCode.Unauthorized, new ResultViewModel<string>("Usuário ou senha inválidos"));

            var token = _tokenService.GenerateToken(user);
            return Ok(new ResultViewModel<string>(token, new List<string>()));
        }
        catch
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResultViewModel<string>("05X04 - Falha interna no servidor"));
        }
    }
}

using DesafioJuntoSeguros.Domain.Entites;
using DesafioJuntoSeguros.Domain.Intefaces;
using DesafioJuntoSeguros.Repository;
using DesafioJuntoSeguros.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DesafioJuntoSeguros.Api
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;

        public LoginController(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> AutenticacaoLogin(Usuario usuario)
        {
            var user = _repository.GetLogin(usuario.Email, usuario.Senha);
            if (user == null)
                return NotFound(new {message = "Login ou senha incorretos"});
            var token = TokenService.GenerateToken(user);
            user.Senha = "";
            return new
            {
                email = user.Email,
                token = token
            };
        }
    }
}

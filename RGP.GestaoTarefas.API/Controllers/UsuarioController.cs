using Microsoft.AspNetCore.Mvc;
using RGP.GestaoTarefas.Application.Handlers.Usuario;
using System.ComponentModel;
using System.Threading.Tasks;

namespace RGP.GestaoTarefas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioCommandHandler _handler;

        public UsuarioController(UsuarioCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("ListarUsuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var result = await _handler.ListarUsuarios();

            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

    }
}

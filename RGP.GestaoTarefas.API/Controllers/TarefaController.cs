using Microsoft.AspNetCore.Mvc;
using RGP.GestaoTarefas.Application.Handlers.Tarefa;
using RGP.GestaoTarefas.Application.Inputs.Tarefa;
using System.Threading.Tasks;

namespace RGP.GestaoTarefas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private TarefaCommandHandler _handler;

        public TarefaController(TarefaCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("ListarTarefas")]
        public async Task<IActionResult> ListarTarefas()
        {
            var result = await _handler.ListarTarefas();

            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("CriarNovaTarefa")]
        public async Task<IActionResult> CriarNovaTarefa(InputTarefaCommand command)
        {
            var result = await _handler.CriarNovaTarefa(command);

            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut("AtualizarStatusTarefa")]
        public async Task<IActionResult> AtualizarStatusTarefa(UpdateTarefaStatusCommand command)
        {
            var result = await _handler.AtualizarStatusTarefa(command);

            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}

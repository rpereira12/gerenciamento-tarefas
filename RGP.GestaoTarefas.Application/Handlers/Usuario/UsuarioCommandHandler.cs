using RGP.GestaoTarefas.Application.Outputs.Usuario;
using RGP.GestaoTarefas.Domain.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RGP.GestaoTarefas.Application.Handlers.Usuario
{
    public class UsuarioCommandHandler
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Result> ListarUsuarios()
        {
            try
            {
                var usuarios = await _usuarioRepository.GetAll();
                var result = usuarios.Select(x =>
                new OutputUsuarioCommand
                {
                    Id = x.Id,
                    Nome = x.Nome,
                }).ToList();

                return new Result(true, result);
            }
            catch (Exception ex)
            {
                return new Result(false, "Ocorreu um erro ao listar os usuários: " + ex.Message);
            }
        }
    }
}

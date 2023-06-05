using RGP.GestaoTarefas.Domain.Entities;
using RGP.GestaoTarefas.Domain.Repository;

namespace RGP.GestaoTarefas.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(RgpDbContext dbContext) : base(dbContext)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RGP.GestaoTarefas.Domain.Entities;
using RGP.GestaoTarefas.Domain.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RGP.GestaoTarefas.Data.Repository
{
    public class TarefaRepository : Repository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(RgpDbContext dbContext) : base(dbContext)
        {

        }

        public new async Task<List<Tarefa>> GetAll()
        {
            return await _dbContext.Set<Tarefa>().Include(x => x.Usuario).ToListAsync();

        }
    }
}

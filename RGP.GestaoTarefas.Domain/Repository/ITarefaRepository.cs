using RGP.GestaoTarefas.Domain.Base;
using RGP.GestaoTarefas.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RGP.GestaoTarefas.Domain.Repository
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        new Task<List<Tarefa>> GetAll();
    }
}

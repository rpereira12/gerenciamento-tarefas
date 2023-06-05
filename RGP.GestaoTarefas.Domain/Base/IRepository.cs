using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RGP.GestaoTarefas.Domain.Base
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetById(string id);

        Task<IEnumerable<TEntity>> GetAll();

        Task Add(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}

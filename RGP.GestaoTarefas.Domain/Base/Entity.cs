namespace RGP.GestaoTarefas.Domain.Base
{
    public abstract class Entity<TEntity> where TEntity : class
    {
        public virtual string Id { get; set; }
    }
}

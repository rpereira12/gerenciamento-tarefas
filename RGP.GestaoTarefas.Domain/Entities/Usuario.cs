using RGP.GestaoTarefas.Domain.Base;
using System.Collections.Generic;

namespace RGP.GestaoTarefas.Domain.Entities
{
    public class Usuario : Entity<Usuario>
    {
        public string Nome { get; set; }
        public ICollection<Tarefa> Tarefas { get; set; }

    }
}

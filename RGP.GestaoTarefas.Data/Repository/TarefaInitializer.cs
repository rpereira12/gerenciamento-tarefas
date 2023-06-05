using RGP.GestaoTarefas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGP.GestaoTarefas.Data.Repository
{
    public static class TarefaInitializer
    {
        public static void Initialize(RgpDbContext context)
        {
            if (!context.Tarefas.Any())
            {
                var tarefas = new List<Tarefa>
                {
                    new Tarefa
                    {
                        Id = Guid.NewGuid().ToString(),
                        DataInicio = new DateTime(2021, 12, 31),
                        Estado = Domain.Value_Objects.EstadoTarefa.Agendada,
                        DuracaoEstimada = new TimeSpan(30, 0, 0),
                        UsuarioId = "1"
                    },
                    new Tarefa
                    {
                        Id = Guid.NewGuid().ToString(),
                        DataInicio = DateTime.Now,
                        Estado = Domain.Value_Objects.EstadoTarefa.EmAndamento,
                        DuracaoEstimada = new TimeSpan(30, 0, 0),
                        UsuarioId = "2"
                    }
                };

                context.Tarefas.AddRange(tarefas);
                context.SaveChanges();
            }
        }

    }
}

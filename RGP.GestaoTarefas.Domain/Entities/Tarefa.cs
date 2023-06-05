using RGP.GestaoTarefas.Domain.Base;
using RGP.GestaoTarefas.Domain.Value_Objects;
using System;

namespace RGP.GestaoTarefas.Domain.Entities
{
    public class Tarefa : Entity<Tarefa>
    {
        public DateTime DataInicio { get; set; }
        public TimeSpan DuracaoEstimada { get; set; }
        public EstadoTarefa Estado { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime? DataConclusao { get; set; }
        public byte[] ArquivoAnexado { get; set; }
    }
}

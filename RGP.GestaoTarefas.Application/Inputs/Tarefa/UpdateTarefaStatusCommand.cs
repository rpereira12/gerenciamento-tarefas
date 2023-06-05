using RGP.GestaoTarefas.Domain.Value_Objects;

namespace RGP.GestaoTarefas.Application.Inputs.Tarefa
{
    public class UpdateTarefaStatusCommand
    {
        public string IdTarefa { get; set; }
        public EstadoTarefa TarefaStatus { get; set; }
    }
}

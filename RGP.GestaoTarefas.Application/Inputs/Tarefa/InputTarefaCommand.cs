using RGP.GestaoTarefas.Domain.Value_Objects;
using System;

namespace RGP.GestaoTarefas.Application.Inputs.Tarefa
{
    public class InputTarefaCommand
    {
        /// <summary>
        /// dd/MM/yyyy
        /// </summary>
        public string DataInicio { get; set; }
        public string DuracaoEstimada { get; set; }
        public string IdUsuario { get; set; }
    }
}

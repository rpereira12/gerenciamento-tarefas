using RGP.GestaoTarefas.Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RGP.GestaoTarefas.Application.Outputs.Tarefa
{
    public class OutputTarefaCommand
    {
        public string Id { get; set; }
        public string DataInicio { get; set; }
        public string DuracaoEstimada { get; set; }
        public string Estado { get; set; }
        public string PessoaAtribuida { get; set; }
        public string DataConclusao { get; set; }
        public string TempoEmAndamento { get; set; }
    }
}

using RGP.GestaoTarefas.Application.Inputs.Tarefa;
using RGP.GestaoTarefas.Application.Outputs.Tarefa;
using RGP.GestaoTarefas.Domain.Repository;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RGP.GestaoTarefas.Application.Handlers.Tarefa
{
    public class TarefaCommandHandler
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TarefaCommandHandler(ITarefaRepository tarefaRepository, IUsuarioRepository usuarioRepository)
        {
            _tarefaRepository = tarefaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Result> CriarNovaTarefa(InputTarefaCommand command)
        {
            try
            {
                var usuario = await _usuarioRepository.GetById(command.IdUsuario);

                if(usuario == null)
                {
                    return new Result(false, $"Usuário não encontrado ID:({command.IdUsuario})");

                }

                var tarefa = new Domain.Entities.Tarefa()
                {
                    Id = Guid.NewGuid().ToString(),
                    DataInicio = ConverterParaDateTime(command.DataInicio),
                    DuracaoEstimada = ConverterParaTimeSpan(command.DuracaoEstimada),
                    Estado = Domain.Value_Objects.EstadoTarefa.Agendada,
                    UsuarioId = usuario.Id,
                    Usuario = usuario
                };

                await _tarefaRepository.Add(tarefa);

                return new Result(true, tarefa.Id, "Dados salvos com sucesso.");
            }
            catch (Exception ex)
            {
                return new Result(false, "Ocorreu um erro ao inserir a tarefa: " + ex.Message);
            }
        }

        public async Task<Result> ListarTarefas()
        {
            try
            {
                var listaDeTarefas = await _tarefaRepository.GetAll();

                var result = listaDeTarefas.Select(x =>
                new OutputTarefaCommand
                {
                    Id = x.Id,
                    DataInicio = ConverterDateTimeParaString(x.DataInicio),
                    DuracaoEstimada = ConverterTimeSpanParaString(x.DuracaoEstimada),
                    Estado = ObterNomeEnum(x.Estado),
                    PessoaAtribuida = x.Usuario.Nome,
                    DataConclusao = x.DataConclusao.HasValue ? ConverterDateTimeParaString(x.DataConclusao.Value) : null,
                    TempoEmAndamento = CalcularTempoEmAndamento(x)
                }).ToList();


                return new Result(true, result);
            }
            catch (Exception ex)
            {
                return new Result(false, "Ocorreu um erro ao listar as tarefas: " + ex.Message);
            }
        }

        public async Task<Result> AtualizarStatusTarefa(UpdateTarefaStatusCommand command)
        {
            try
            {
                var tarefa = await _tarefaRepository.GetById(command.IdTarefa);

                if(tarefa == null)
                {
                    return new Result(false, "Tarefa não encontrada");
                }

                if (tarefa.Estado == Domain.Value_Objects.EstadoTarefa.EmAndamento && command.TarefaStatus == Domain.Value_Objects.EstadoTarefa.Agendada)
                {
                    return new Result(false, "Não é permitido alterar uma tarefa em andamento para agendada.");
                }

                if (tarefa.Estado == Domain.Value_Objects.EstadoTarefa.Concluida && command.TarefaStatus == Domain.Value_Objects.EstadoTarefa.Agendada)
                {
                    return new Result(false, "Não é permitido alterar uma tarefa concluída para agendada.");
                }

                if (tarefa.Estado == Domain.Value_Objects.EstadoTarefa.Concluida && command.TarefaStatus == Domain.Value_Objects.EstadoTarefa.EmAndamento)
                {
                    return new Result(false, "Não é permitido alterar uma tarefa concluída para em andamento.");
                }

                tarefa.Estado = command.TarefaStatus;

                await _tarefaRepository.Update(tarefa);
                return new Result(true, "Tarefa atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return new Result(false, "Ocorreu um erro ao atualizar a tarefa: " + ex.Message);
            }
        }

        #region [ Métodos de validação ] 
        public TimeSpan ConverterParaTimeSpan(string horaMinuto)
        {
            if (string.IsNullOrWhiteSpace(horaMinuto))
                throw new ArgumentException("A string não pode ser nula ou vazia.", nameof(horaMinuto));

            string[] partes = horaMinuto.Split(':');

            if (partes.Length != 2)
                throw new FormatException("A string deve estar no formato 'HH:MM'.");

            if (!int.TryParse(partes[0], out int horas) || !int.TryParse(partes[1], out int minutos))
                throw new FormatException("A string deve estar no formato 'HH:MM' com valores numéricos válidos.");

            return new TimeSpan(horas, minutos, 0);
        }

        public string ConverterTimeSpanParaString(TimeSpan timeSpan)
        {
            int totalHoras = (int)timeSpan.TotalHours;
            int minutes = timeSpan.Minutes;

            return $"{totalHoras:00}:{minutes:00}";
        }



        public DateTime ConverterParaDateTime(string dataString)
        {
            if (string.IsNullOrWhiteSpace(dataString))
                throw new ArgumentException("A string não pode ser nula ou vazia.", nameof(dataString));

            if (!DateTime.TryParseExact(dataString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data))
                throw new FormatException("A string deve estar no formato 'dd/MM/yyyy'.");

            return data;
        }

        public string ConverterDateTimeParaString(DateTime data)
        {
            return data.ToString("dd/MM/yyyy");
        }

        public string ObterNomeEnum<TEnum>(TEnum valorEnum) where TEnum : Enum
        {
            return Enum.GetName(typeof(TEnum), valorEnum);
        }

        public string CalcularTempoEmAndamento(Domain.Entities.Tarefa tarefa)
        {
            if (tarefa.Estado == Domain.Value_Objects.EstadoTarefa.EmAndamento)
            {
                TimeSpan tempoEmAndamento = DateTime.Now - tarefa.DataInicio;
                return tempoEmAndamento.ToString(@"hh\:mm\:ss");
            }

            return string.Empty;
        }

        #endregion
    }
}

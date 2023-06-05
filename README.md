# Gerenciamento de tarefas

Projeto desenvolvido para Trixx para avaliação.

## Observações

- Foi utilizado o Banco de dados InMemory
- Na camada de infra/Data/Repository é possível verificar os dados que estão sendo adicionados para inicializar o banco, todos seguiram as especificações do PDF da prova.

## Função das APIs

| API | Observações |
| ------ | ------ |
| /Tarefa/ListarTarefas | Lista tarefas cadastradas. Caso a tarefa esteja com status EmAndamento é contabilizado o tempo de andamento com base na dataInicio e dataAtual. |
| /Tarefa/CriarNovaTarefa | Cria uma nova tarefa com base nos dados fornecidos. Formato de data aceito em string 'dd/MM/yyyy', Formato de duração aceito: 'HH:MM'  |
| /Tarefa/AtualizarStatusTarefa | Atualiza o status da tarefa com base no Enum: 0 - Agendada, 1 - EmAndamento, 2 - Concluida. Não é possível regredir as fases da tarefa, há essa validação. |
| /Tarefa/ListarUsuarios | Lista os usuários: Id e Nome.  |

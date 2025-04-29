using System.Collections.Generic;
using System.Threading.Tasks;
using apiToDo.DTO;
using apiToDo.Models;

namespace apiToDo.Services
{   
    public interface ITarefasService
    {
        Task<List<TarefaDTO>> ListarTarefas();
        Task<TarefaDTO> VerTarefa(int ID_TAREFA);
        Task<List<TarefaDTO>> InserirTarefa(InserirTarefaDTO DS_TAREFA);
        Task<List<TarefaDTO>> AtualizarTarefa(TarefaDTO Request);
        Task<List<TarefaDTO>> DeletarTarefa(int ID_TAREFA);
    }
    public class TarefasService:ITarefasService
    {
        private readonly Tarefas _tarefas;
        public TarefasService(Tarefas tarefas)
        {
            _tarefas = tarefas;
        }
        public async Task<List<TarefaDTO>> ListarTarefas()
        {
            List<TarefaDTO> tarefas = await _tarefas.ListarTarefas();
            return tarefas;

        }
        public async Task<TarefaDTO> VerTarefa(int ID_TAREFA)
        {
            TarefaDTO tarefa = await _tarefas.BuscarTarefaPorId(ID_TAREFA);
            return tarefa;

        }

        public async Task<List<TarefaDTO>> InserirTarefa(InserirTarefaDTO Request)
        {
            // Recebe do controller do request e envia a descrição para o Models que está controlando o banco de dados, sendo assim ele adiciona uma nova tarefa.
            List<TarefaDTO> tarefas = await _tarefas.InserirTarefa(Request.DS_TAREFA);
            return tarefas;

        }
        public async Task<List<TarefaDTO>> AtualizarTarefa(TarefaDTO Request) 
        {
            TarefaDTO tarefa = await _tarefas.BuscarTarefaPorId(Request.ID_TAREFA);

            if (tarefa == null)
                return null; 

            // Somente atualiza o "banco de dados" se encontrar diferença nas descrições, caso contrário, não o faz.
            if (tarefa.DS_TAREFA != Request.DS_TAREFA)
            {
                tarefa.DS_TAREFA = Request.DS_TAREFA;
                await _tarefas.AtualizarTarefa(tarefa);
            }

            return await _tarefas.ListarTarefas();

        }

        public async Task<List<TarefaDTO>> DeletarTarefa(int ID_TAREFA)
        {
            // No primeiro momento o sistema, na camada de negócios, busca por ID a tarefa. Nesse caso, ele pode retornar a tarefa ou Null.
            TarefaDTO tarefa = await _tarefas.BuscarTarefaPorId(ID_TAREFA);
            // Se o sistema não encontrar a tarefa, será retornado null
            if (tarefa == null)
                return null;

            // Se o sistema encontrar a tarefa, ele passará para o "repositório de dados" a tarefa encontrada para que possa removê-lo da lista
            await _tarefas.DeletarTarefa(tarefa);
            
            // Como a API encontrou a tarefa, ele vai atualizar o usuário com as tarefas ainda que não foram deletadas 
            return await _tarefas.ListarTarefas();
        }

    }
}

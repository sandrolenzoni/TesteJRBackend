using System.Collections.Generic;
using System.Threading.Tasks;
using apiToDo.DTO;
using apiToDo.Models;

namespace apiToDo.Services
{   
    public interface ITarefasService
    {
        Task<List<TarefaDTO>> ListarTarefas();
        Task<List<TarefaDTO>> InserirTarefa(TarefaDTO tarefa);
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

        public async Task<List<TarefaDTO>> InserirTarefa(TarefaDTO tarefa)
        {
            List<TarefaDTO> tarefas = await _tarefas.ListarTarefas();
            return tarefas;

        }

    }
}

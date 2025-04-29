using apiToDo.DTO;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace apiToDo.Models
{
    public class Tarefas
    {
        private static readonly List<TarefaDTO> _tarefas = new List<TarefaDTO>
             {
        new TarefaDTO { ID_TAREFA = 1, DS_TAREFA = "Fazer Compras" },
        new TarefaDTO { ID_TAREFA = 2, DS_TAREFA = "Fazer Atividade da Faculdade" },
        new TarefaDTO { ID_TAREFA = 3, DS_TAREFA = "Subir Projeto de Teste no GitHub" }
    };
        public async Task<List<TarefaDTO>> ListarTarefas()
        {
            try
            {
                return await Task.FromResult(_tarefas);
            }
            catch(Exception)
            {
                throw;
            }
        }


        public async Task<List<TarefaDTO>> InserirTarefa(String ds_tarefa)
        {
            try
            {
                // O ID é gerado pelo método Tarefa.
                TarefaDTO Request = new TarefaDTO
                {
                    ID_TAREFA = _tarefas.Max(tarefa => tarefa.ID_TAREFA) + 1,
                    DS_TAREFA = ds_tarefa
                };
                _tarefas.Add(Request);
                return await Task.FromResult(_tarefas);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<bool> DeletarTarefa(int ID_TAREFA)
        {
            try
            {
                // Busca a tarefa no "banco de dados" que está sendo armazenado na memória do sistema
                TarefaDTO tarefa = _tarefas.FirstOrDefault(tarefa => tarefa.ID_TAREFA == ID_TAREFA);
                // Se não existir a tarefa, vai retornar falso
                if (tarefa == null)
                    return false;
                
                _tarefas.Remove(tarefa);
                // Se existir a tarefa, o método vai retornar true
                return await Task.FromResult(true);

            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}

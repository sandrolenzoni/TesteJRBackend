using apiToDo.DTO;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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
        public async Task<bool> DeletarTarefa(TarefaDTO tarefa)
        {
            try
            {
                // O sistema, que já encontrou a tarefa, apenas a deleta do sistema e retorna informando que foi deletado       
                _tarefas.Remove(tarefa);
                // Se existir a tarefa, o método vai retornar true
                return await Task.FromResult(true);

            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<char> AtualizarTarefa(TarefaDTO tarefaAtualizada)
        {
            int index = _tarefas.FindIndex(tarefa => tarefa.ID_TAREFA == tarefaAtualizada.ID_TAREFA);
            if (index >=0)
            {
                _tarefas[index] = tarefaAtualizada;
                return await Task.FromResult('1');
            }

            return await Task.FromResult('0');
        }

        public async Task<TarefaDTO > BuscarTarefaPorId(int ID_TAREFA)
        {
            try
            {
                // Busca a tarefa no "banco de dados" que está sendo armazenado na memória do sistema
                TarefaDTO tarefa = _tarefas.FirstOrDefault(tarefa => tarefa.ID_TAREFA == ID_TAREFA);
            

                // Se existir a vai retorná-la, se não irá retornar null
                return await Task.FromResult(tarefa);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

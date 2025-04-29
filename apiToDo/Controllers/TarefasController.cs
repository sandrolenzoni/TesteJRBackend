using apiToDo.DTO;
using apiToDo.Models;
using apiToDo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiToDo.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {

        private readonly ITarefasService _tarefaService;

        public TarefasController(ITarefasService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TarefaDTO>>> ListarTarefas()
        {
            try
            {

                List<TarefaDTO> _tarefas = await _tarefaService.ListarTarefas();
                return Ok(_tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<List<TarefaDTO>>> VerTarefa(int id)
        {
            try
            {

                TarefaDTO tarefa = await _tarefaService.VerTarefa(id);
                if (tarefa == null)
                    return NotFound(new { msg = $"A tarefa de código {id} não existe" });

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<TarefaDTO>>> InserirTarefas([FromBody] InserirTarefaDTO Request)
        {
            try
            {
                // Testa para verificar se a descrição da tarefa está vazia, porque é impedimento. 
                if (Request == null || string.IsNullOrEmpty(Request.DS_TAREFA))
                    return BadRequest(new { msg = "A descrição da tarefa não pode ser vazia." });

                // Caso não haja nenhum impedimento, envia para o serviço o DTO de Inserir Tarefa
                List<TarefaDTO> tarefas = await _tarefaService.InserirTarefa(Request);
                return Ok(tarefas);
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }
        [HttpPatch]
        public async Task<ActionResult<List<TarefaDTO>>> UpdateTask([FromBody] TarefaDTO Request)
        {
            try
            {
                List<TarefaDTO> tarefas = await _tarefaService.AtualizarTarefa(Request);

                // Caso não encontra, o sistema retorna um erro de 404, informando que o ID não foi encontrado
                if (tarefas == null)
                    return NotFound(new { msg = $"A tarefa de código {Request.ID_TAREFA} não foi encontrada" });

                // Por outro lado, se o sistema conseguir encontrar a tarefa, ele irá retornar a lista;
                return Ok(tarefas);
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }

        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteTask([FromQuery] int id)
        {
            try
            {
                // Chama a camada de serviço, passando o ID enviado pelo usuário
                List<TarefaDTO> tarefas = await _tarefaService.DeletarTarefa(id);

                // Caso não encontra, o sistema retorna um erro de 404, informando que o ID não foi encontrado
                if (tarefas == null)
                    return NotFound(new { msg = $"A tarefa de código {id} não foi encontrada" });
                 
                // Por outro lado, se o sistema conseguir encontrar a tarefa, ele irá retornar a lista;
                return Ok(tarefas); 
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }
    }
}

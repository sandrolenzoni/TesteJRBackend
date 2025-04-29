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
        public async Task<ActionResult<List<TarefaDTO>>> GetTarefas()
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

        [HttpDelete]
        public ActionResult DeleteTask([FromQuery] int ID_TAREFA)
        {
            try
            {

                return StatusCode(200);
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }
    }
}

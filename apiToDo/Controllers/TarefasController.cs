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
        public async Task<ActionResult<TarefaDTO>> InserirTarefas([FromBody] TarefaDTO Request)
        {
            try
            {
                if (Request == null || string.IsNullOrEmpty(Request.DS_TAREFA))
                    return BadRequest(new { msg = "Dados inválidos da tarefa" });
                

                return StatusCode(200);


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

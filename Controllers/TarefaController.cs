using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
           // TODO: implementado
            var _tafefa = _context.Tarefas.Find(id);

            if(_tafefa is null)
              return NotFound();

            return Ok(_tafefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var _tarefaAll = _context.Tarefas.ToList();
            // TODO: implementado
            if(_tarefaAll.Count > 0)
               return Ok(_tarefaAll);
            else
               return NotFound(); 
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: implementado

            var _tarefa = _context.Tarefas.Where(W => W.Titulo.StartsWith(titulo) ).ToList();

            if(_tarefa.Count > 0)
               return Ok(_tarefa);
            else
               return NotFound(); 
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: implementado
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {

           // TODO: implementado

           if(!ModelState.IsValid)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
           // TODO: implementado

            if(!ModelState.IsValid)
                    return BadRequest(new { Erro = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList()  });

            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco is null)
                return NotFound();
            else
                _context.Tarefas.Update(tarefa);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
             // TODO: implementado
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco is null)
                return NotFound();

            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

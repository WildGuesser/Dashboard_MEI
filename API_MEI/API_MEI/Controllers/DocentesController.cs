using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_MEI.Data;
using API_MEI.Models;

namespace API_MEI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocentesController : ControllerBase
    {
        private readonly API_MEIContext _context;

        public DocentesController(API_MEIContext context)
        {
            _context = context;
        }

        // GET: Docentes
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var docentes = await _context.Docentes.ToListAsync();
            return Ok(docentes);
        }

        // GET: Docentes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocente(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);

            if (docente == null)
            {
                return NotFound($"Docente com ID {id} não encontrado.");
            }

            return Ok(docente);
        }

        // POST: Docentes
        [HttpPost("Create")]
        public async Task<IActionResult> CreateDocente(Docentes docente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(docente);
                    await _context.SaveChangesAsync();
                    return Ok("Docente criado com sucesso!");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao criar docente: {ex.Message}");
                }
            }
            else
            {
                return BadRequest($"Erro ao criar docente: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        // PUT: Docentes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Docentes docente)
        {
            if (id != docente.Id)
            {
                return BadRequest("O ID do docente a ser atualizado não corresponde ao ID fornecido.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(docente);
                    await _context.SaveChangesAsync();
                    return Ok("Docente atualizado com sucesso!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocenteExists(id))
                    {
                        return NotFound($"Docente com ID {id} não encontrado.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return BadRequest($"Erro ao atualizar docente: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        // DELETE: Docentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocente(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);
            if (docente == null)
            {
                return NotFound($"Docente com ID {id} não encontrado.");
            }

            _context.Docentes.Remove(docente);
            await _context.SaveChangesAsync();

            return Ok("Docente excluído com sucesso!");
        }

        private bool DocenteExists(int id)
        {
            return _context.Docentes.Any(e => e.Id == id);
        }

        [HttpPost("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Nenhum ID de aluno foi fornecido para exclusão múltipla.");
            }

            var docentesToDelete = await _context.Docentes.Where(a => ids.Contains(a.Id)).ToListAsync();

            if (docentesToDelete == null || docentesToDelete.Count == 0)
            {
                return NotFound("Nenhum aluno encontrado com os IDs fornecidos para exclusão múltipla.");
            }

            try
            {
                _context.Docentes.RemoveRange(docentesToDelete);
                await _context.SaveChangesAsync();
                return Ok($"Exclusão múltipla de {docentesToDelete.Count} alunos concluída com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao excluir múltiplos alunos: {ex.Message}");
            }
        }
    }
}

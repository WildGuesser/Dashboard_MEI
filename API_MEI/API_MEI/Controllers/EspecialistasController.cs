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
    public class EspecialistasController : ControllerBase
    {
        private readonly API_MEIContext _context;

        public EspecialistasController(API_MEIContext context)
        {
            _context = context;
        }

        // GET: Especialistas
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var especialistas = await _context.Especialistas.Include(e => e.Empresas).ToListAsync();
            return Ok(especialistas);
        }

        // GET: Especialistas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEspecialista(int id)
        {
            var especialista = await _context.Especialistas.Include(e => e.Empresas).SingleOrDefaultAsync(e => e.Empresa_ID == id);

            if (especialista == null)
            {
                return NotFound($"Especialista com ID {id} não encontrado.");
            }

            return Ok(especialista);
        }

        // POST: Especialistas
        [HttpPost("Create")]
        public async Task<IActionResult> CreateEspecialista(Especialistas especialista)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(especialista);
                    await _context.SaveChangesAsync();
                    return Ok("Especialista criado com sucesso!");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao criar especialista: {ex.Message}");
                }
            }
            else
            {
                return BadRequest($"Erro ao criar especialista: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        // PUT: Especialistas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Especialistas especialista)
        {
            if (id != especialista.Id)
            {
                return BadRequest("O ID do especialista a ser atualizado não corresponde ao ID fornecido.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialista);
                    await _context.SaveChangesAsync();
                    return Ok("Especialista atualizado com sucesso!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialistaExists(id))
                    {
                        return NotFound($"Especialista com ID {id} não encontrado.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return BadRequest($"Erro ao atualizar especialista: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        // DELETE: Especialistas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialista(int id)
        {
            var especialista = await _context.Especialistas.FindAsync(id);
            if (especialista == null)
            {
                return NotFound($"Especialista com ID {id} não encontrado.");
            }

            _context.Especialistas.Remove(especialista);
            await _context.SaveChangesAsync();

            return Ok("Especialista excluído com sucesso!");
        }

        private bool EspecialistaExists(int id)
        {
            return _context.Especialistas.Any(e => e.Id == id);
        }

        [HttpPost("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Nenhum ID de aluno foi fornecido para exclusão múltipla.");
            }

            var especialistasToDelete = await _context.Especialistas.Where(a => ids.Contains(a.Id)).ToListAsync();

            if (especialistasToDelete == null || especialistasToDelete.Count == 0)
            {
                return NotFound("Nenhum aluno encontrado com os IDs fornecidos para exclusão múltipla.");
            }

            try
            {
                _context.Especialistas.RemoveRange(especialistasToDelete);
                await _context.SaveChangesAsync();
                return Ok($"Exclusão múltipla de {especialistasToDelete.Count} alunos concluída com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao excluir múltiplos alunos: {ex.Message}");
            }
        }
    }
}

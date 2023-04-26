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
    public class TrabalhosController : ControllerBase
    {
        private readonly API_MEIContext _context;

        public TrabalhosController(API_MEIContext context)
        {
            _context = context;
        }

        // GET: Trabalhos
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var trabalhos = await _context.Trabalhos
                .Include(a => a.Alunos)
                .Include(j => j.Juri).ThenInclude(jm => jm.JuriMembros).ThenInclude(m => m.Membros)
                .Include(o => o.Orientadores).ThenInclude(o => o.Membros)
                .Include(e => e.Empresas)
                .ToListAsync();

            return Ok(trabalhos);
        }

        // GET: Trabalhos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrabalho(int id)
        {
            var trabalho = await _context.Trabalhos
                .Include(t => t.Juri).ThenInclude(l=>l.JuriMembros).ThenInclude(m => m.Membros)
                .Include(t => t.Alunos)
                .Include(t => t.Orientadores).ThenInclude(o=>o.Membros)
                .Include(t => t.Empresas)
                .SingleOrDefaultAsync(t => t.Id == id);

            if (trabalho == null)
            {
                return NotFound($"Trabalho com ID {id} não encontrado.");
            }

            return Ok(trabalho);
        }

        // POST: Trabalhos
        [HttpPost("Create")]
        public async Task<IActionResult> CreateTrabalho(Trabalhos input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(input);
                    await _context.SaveChangesAsync();

                    return Ok("Trabalho criado com sucesso!");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao criar Trabalho: {ex.Message}");
                }
            }
            else
            {
                return BadRequest($"Erro ao criar Trabalho: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        // PUT: Trabalhos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Trabalhos input)
        {
            if (id != input.Id)
            {
                return BadRequest("O ID do trabalho a ser atualizado não corresponde ao ID fornecido.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(input);
                    await _context.SaveChangesAsync();

                    return Ok("Trabalho atualizado com sucesso!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabalhoExists(id))
                    {
                        return NotFound($"Trabalho com ID {id} não encontrado.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return BadRequest($"Erro ao atualizar trabalho: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        private bool TrabalhoExists(int id)
        {
            return _context.Trabalhos.Any(e => e.Id == id);
        }


        // DELETE: Trabalhos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrabalho(int id)
        {
            var trabalho = await _context.Trabalhos.FindAsync(id);
            if (trabalho == null)
            {
                return NotFound($"Trabalho com ID {id} não encontrado.");
            }

            // Remove the Trabalhos record
            _context.Trabalhos.Remove(trabalho);
            await _context.SaveChangesAsync();

            return Ok("Trabalho excluído com sucesso!");
        }

        [HttpPost("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Nenhum ID de trabalho foi fornecido para exclusão múltipla.");
            }

            var trabalhosToDelete = await _context.Trabalhos.Where(a => ids.Contains(a.Id)).ToListAsync();

            if (trabalhosToDelete == null || trabalhosToDelete.Count == 0)
            {
                return NotFound("Nenhum trabalho encontrado com os IDs fornecidos para exclusão múltipla.");
            }

            try
            {
                _context.Trabalhos.RemoveRange(trabalhosToDelete);
                await _context.SaveChangesAsync();
                return Ok($"Exclusão múltipla de {trabalhosToDelete.Count} trabalhos concluída com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao excluir múltiplos trabalhos: {ex.Message}");
            }
        }
        //Membros
        // GET: api/Membros
        [HttpGet("Membros/List")]
        public async Task<IActionResult> Membros_List()
        {
            var membros = await _context.Membros
                .ToListAsync();

            return Ok(membros);
        }

        // GET: Trabalhos/Orientador/5
        [HttpGet("Membros/{id}")]
        public async Task<IActionResult> GetOrientador(int id)
        {
            var membro = await _context.Membros
                .SingleOrDefaultAsync(t => t.Id == id);

            if (membro == null)
            {
                return NotFound($"Orientador com ID {id} não encontrado.");
            }

            return Ok(membro);
        }
    }
}

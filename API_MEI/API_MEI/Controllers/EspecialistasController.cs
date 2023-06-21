using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_MEI.Data;
using API_MEI.Models;
using Microsoft.AspNetCore.Authorization;

namespace API_MEI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

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
            var especialistas = await _context.Especialistas
                .Include(e => e.Empresas)
                .Include(e => e.Membros)
                .ToListAsync();

            return Ok(especialistas);
        }

        // GET: Especialistas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEspecialista(int id)
        {
            var especialista = await _context.Especialistas.Include(e => e.Empresas).Include(e => e.Membros).SingleOrDefaultAsync(e => e.Membros.Id == id);

            if (especialista == null)
            {
                return NotFound($"Especialista com ID {id} não encontrado.");
            }

            return Ok(especialista);
        }

        // POST: Especialistas
        [HttpPost("Create")]
        public async Task<IActionResult> CreateEspecialista(Especialistas input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // create new Membros entity
                    var membros = new Membros
                    {
                        Nome = input.Membros.Nome,
                        Email = input.Membros.Email,
                        Contacto = input.Membros.Contacto
                    };
                    _context.Membros.Add(membros);
                    await _context.SaveChangesAsync();
                    // create new Especialistas entity with same Id as Membros
                    var especialista = new Especialistas
                    {
                        Id = membros.Id,
                        Empresa_Id = input.Empresa_Id,
                    };
                    _context.Add(especialista);
                    await _context.SaveChangesAsync();

                    return Ok("Especialista criado com sucesso!");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao criar Especialiesta: {ex.Message}");
                }
            }
            else
            {
                return BadRequest($"Erro ao criar Especialista: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        // PUT: Especialistas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Especialistas input)
        {
            if (id != input.Id)
            {
                return BadRequest("O ID do especialista a ser atualizado não corresponde ao ID fornecido.");
            }

            var especialista = await _context.Especialistas.FindAsync(id);
            if (especialista == null)
            {
                return NotFound($"Especialista com ID {id} não encontrado.");
            }

            var EMembro = await _context.Membros.FindAsync(id);
            if (EMembro == null)
            {
                return NotFound($"Membro com ID {id} não encontrado.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(especialista).State = EntityState.Detached;
                    _context.Entry(EMembro).State = EntityState.Detached;
                    // create new Membros entity
                    var membros = new Membros
                    {
                        Id = input.Id,
                        Nome = input.Membros.Nome,
                        Email = input.Membros.Email,
                        Contacto = input.Membros.Contacto
                    };
                    _context.Update(membros);
                    await _context.SaveChangesAsync();

                    _context.Entry(membros).State = EntityState.Detached;
                    // create new Especialistas entity with same Id as Membros
                    var especialistaAtualizado = new Especialistas
                    {
                        Id = membros.Id,
                        Empresa_Id = input.Empresa_Id,
                    };
                    _context.Update(especialistaAtualizado);
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

        private bool EspecialistaExists(int id)
        {
            return _context.Especialistas.Any(e => e.Id == id);
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

            // Remove the Especialistas record
            _context.Especialistas.Remove(especialista);
            await _context.SaveChangesAsync();

            // Find the corresponding Membros record
            var membro = await _context.Membros.FindAsync(especialista.Id);
            if (membro != null)
            {
                // Remove the Membros record first
                _context.Membros.Remove(membro);
                await _context.SaveChangesAsync();
            }


            return Ok("Especialista excluído com sucesso!");
        }

        [HttpPost("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Nenhum ID de especialista foi fornecido para exclusão múltipla.");
            }

            var especialistasToDelete = await _context.Especialistas.Where(a => ids.Contains(a.Id)).ToListAsync();

            if (especialistasToDelete == null || especialistasToDelete.Count == 0)
            {
                return NotFound("Nenhum especialista encontrado com os IDs fornecidos para exclusão múltipla.");
            }

            // Find the IDs of the corresponding Membros
            var membrosToDelete = await _context.Membros
                .Where(m => especialistasToDelete.Select(d => d.Id).Contains(m.Id))
                .ToListAsync();

            try
            {
                _context.Especialistas.RemoveRange(especialistasToDelete);
                _context.Membros.RemoveRange(membrosToDelete);
                await _context.SaveChangesAsync();
                return Ok($"Exclusão múltipla de {especialistasToDelete.Count} especialistas concluída com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao excluir múltiplos especialistas: {ex.Message}");
            }
        }
    }
}

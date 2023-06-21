using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_MEI.Data;
using API_MEI.Models;
using AutoMapper;
using API_MEI.DTOs.Tabela_Docente;
using API_MEI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace API_MEI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class DocentesController : ControllerBase
    {
        private readonly API_MEIContext _context;
        private readonly IMapper _mapper;
        public DocentesController(API_MEIContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: Docentes
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var docentes = await _context.Docentes
                .Include(d => d.Membros)
                .ToListAsync();

            return Ok(docentes);
        }

        // GET: Docentes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocente(int id)
        {
            var docente = await _context.Docentes
                .Include(d => d.Membros)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (docente == null)
            {
                return NotFound($"Docente com ID {id} não encontrado.");
            }

            return Ok(new
            {
                docente.Id,
                docente.Filiacao,
                Membros = new
                {
                    docente.Membros.Id,
                    docente.Membros.Nome,
                    docente.Membros.Email,
                    docente.Membros.Contacto
                }
            });
        }

        // POST: Docentes
        [HttpPost("Create")]
        public async Task<IActionResult> CreateDocente(DocentesDTO input)
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

                    // create new Docentes entity with same Id as Membros
                    var docente = new Docentes
                    {
                        Id = membros.Id,
                        Filiacao = input.Filiacao,
                    };
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
        public async Task<IActionResult> Edit(int id, Docentes input)
        {
            if (id != input.Id)
            {
                return BadRequest("O ID do docente a ser atualizado não corresponde ao ID fornecido.");
            }

            var Edocente = await _context.Docentes.FindAsync(id);
            if (Edocente == null)
            {
                return NotFound($"Docente com ID {id} não encontrado.");
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
                    _context.Entry(Edocente).State = EntityState.Detached;
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
                    // create new Docentes entity with same Id as Membros
                    var docente = new Docentes
                    {
                        Id = membros.Id,
                        Filiacao = input.Filiacao,
                    };
                    _context.Update(docente);
                    await _context.SaveChangesAsync();

                    return Ok("Docente criado com sucesso!");
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

        private bool DocenteExists(int id)
        {
            return _context.Docentes.Any(e => e.Id == id);
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

            // Remove the Docentes record
            _context.Docentes.Remove(docente);
            await _context.SaveChangesAsync();

            // Find the corresponding Membros record
            var membro = await _context.Membros.FindAsync(docente.Id);
            if (membro != null)
            {
                // Remove the Membros record first
                _context.Membros.Remove(membro);
                await _context.SaveChangesAsync();
            }


            return Ok("Docente excluído com sucesso!");
        }

        [HttpPost("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Nenhum ID de docente foi fornecido para exclusão múltipla.");
            }

            var docentesToDelete = await _context.Docentes.Where(a => ids.Contains(a.Id)).ToListAsync();

            if (docentesToDelete == null || docentesToDelete.Count == 0)
            {
                return NotFound("Nenhum docente encontrado com os IDs fornecidos para exclusão múltipla.");
            }

            // Find the IDs of the corresponding Membros
            var membrosToDelete = await _context.Membros
                .Where(m => docentesToDelete.Select(d => d.Id).Contains(m.Id))
                .ToListAsync();

            try
            {
                _context.Docentes.RemoveRange(docentesToDelete);
                _context.Membros.RemoveRange(membrosToDelete);
                await _context.SaveChangesAsync();
                return Ok($"Exclusão múltipla de {docentesToDelete.Count} docentes concluída com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao excluir múltiplos docentes: {ex.Message}");
            }
        }

    }
}

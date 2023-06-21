using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_MEI.Data;
using API_MEI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;


namespace API_MEI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class JuriMembrosController : ControllerBase
    {
        private readonly API_MEIContext _context;
        private readonly IMapper _mapper;

        public JuriMembrosController(API_MEIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: JuriMembros
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var juriMembrosList = await _context.JuriMembros.ToListAsync();

            if (juriMembrosList == null || juriMembrosList.Count == 0)
            {
                return NotFound("Nenhum júri encontrado.");
            }

            return Ok(juriMembrosList);
        }

        // GET: JuriMembros/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJuriMembrosById(int id)
        {
            var juriMembros = await _context.JuriMembros.FindAsync(id);

            if (juriMembros == null)
            {
                return NotFound($"Júri com ID {id} não encontrado.");
            }

            return Ok(juriMembros);
        }

        // POST: JuriMembros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        public async Task<IActionResult> Create(JuriMembros input)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(input);
                    await _context.SaveChangesAsync();

                    
                    return Ok("Aluno criado com sucesso!");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao criar JuriMembros: {ex.Message}");
                }
            }
            else
            {
                // retorna uma mensagem de erro com os detalhes do modelo inválido
                return BadRequest($"Erro ao criar júri: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }
        [HttpPut("{juriId}/{membroId}")]
        public async Task<IActionResult> Edit(int juriId, int membroId, JuriMembros juriMembros)
        {
            var existingJuriMembros = await _context.JuriMembros.FindAsync(juriId, membroId);
            if (existingJuriMembros == null)
            {
                return NotFound($"JuriMembros with Juri_Id {juriId} and Membro_Id {membroId} not found.");
            }

            try
            {
                // Remove the existing JuriMembros record
                _context.JuriMembros.Remove(existingJuriMembros);
                await _context.SaveChangesAsync();

                // Add the new JuriMembros record
                _context.JuriMembros.Add(juriMembros);
                await _context.SaveChangesAsync();

                return Ok(existingJuriMembros);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Error saving to the database: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }


        [HttpDelete("{juriId}/{membroId}")]
        public async Task<IActionResult> Delete(int juriId, int membroId)
        {
            if (_context.JuriMembros == null)
            {
                return Problem("Entity set 'API_MEIContext.JuriMembross' is null.");
            }
            var existingJuriMembros = await _context.JuriMembros.FindAsync(juriId, membroId);

            if (existingJuriMembros != null)
            {
                _context.JuriMembros.Remove(existingJuriMembros);
                await _context.SaveChangesAsync();
                return Ok("Eliminado com sucesso");
            }

            return NotFound("ERRO - Não foi encontrada nenhum JuriMembros");
        }

    }
}



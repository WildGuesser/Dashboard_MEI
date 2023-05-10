using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_MEI.Data;
using API_MEI.Models;
using AutoMapper;

namespace API_MEI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrientadoresController : ControllerBase
    {
        private readonly API_MEIContext _context;
        private readonly IMapper _mapper;

        public OrientadoresController(API_MEIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Orientadores
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var orientadoresList = await _context.Orientadores.ToListAsync();

            if (orientadoresList == null || orientadoresList.Count == 0)
            {
                return NotFound("Nenhum júri encontrado.");
            }

            return Ok(orientadoresList);
        }

        // GET: Orientadores/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrientadoresById(int id)
        {
            var orientadores = await _context.Orientadores.FindAsync(id);

            if (orientadores == null)
            {
                return NotFound($"Júri com ID {id} não encontrado.");
            }

            return Ok(orientadores);
        }

        // POST: Orientadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Orientadores input)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(input);
                    await _context.SaveChangesAsync();


                    return Ok("Orientador criado com sucesso!");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao criar Orientadores: {ex.Message}");
                }
            }
            else
            {
                // retorna uma mensagem de erro com os detalhes do modelo inválido
                return BadRequest($"Erro ao criar júri: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        [HttpPut("{trabalhoId}/{membroId}")]
        public async Task<IActionResult> Edit(int trabalhoId, int membroId, Orientadores orientadores)
        {
            var existingOrientadores = await _context.Orientadores.FindAsync(trabalhoId, membroId);

            try
            {
                if(existingOrientadores != null)
                { 
                    // Remove the existing JuriMembros record
                    _context.Orientadores.Remove(existingOrientadores);
                    await _context.SaveChangesAsync();
                }

                // Add the new JuriMembros record
                _context.Orientadores.Add(orientadores);
                await _context.SaveChangesAsync();

                return Ok(orientadores);
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



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Orientadores == null)
            {
                return Problem("Entity set 'API_MEIContext.Orientadoress' is null.");
            }
            var orientadores = await _context.Orientadores.FindAsync(id);

            if (orientadores != null)
            {
                _context.Orientadores.Remove(orientadores);
                await _context.SaveChangesAsync();
                return Ok("Eliminado com sucesso");
            }

            return NotFound("ERRO - Não foi encontrada nenhum Orientadores");
        }

    }
}



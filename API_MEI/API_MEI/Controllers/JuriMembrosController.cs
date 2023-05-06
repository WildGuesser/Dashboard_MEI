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

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, JuriMembros juriMembros)
        {
            var existingJuriMembros = await _context.JuriMembros.FindAsync(id);
            if (existingJuriMembros == null)
            {
                return NotFound($"JuriMembros com ID {id} não encontrado.");
            }

            try
            {

                _context.Entry(existingJuriMembros).State = EntityState.Detached;

                _context.Update(juriMembros);

                await _context.SaveChangesAsync();

                return Ok(juriMembros);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Erro ao salvar no banco de dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.JuriMembros == null)
            {
                return Problem("Entity set 'API_MEIContext.JuriMembross' is null.");
            }
            var juriMembros = await _context.JuriMembros.FindAsync(id);

            if (juriMembros != null)
            {
                _context.JuriMembros.Remove(juriMembros);
                await _context.SaveChangesAsync();
                return Ok("Eliminado com sucesso");
            }

            return NotFound("ERRO - Não foi encontrada nenhum JuriMembros");
        }

    }
}



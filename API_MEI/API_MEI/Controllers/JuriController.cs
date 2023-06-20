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

    public class JuriController : ControllerBase
    {
        private readonly API_MEIContext _context;
        private readonly IMapper _mapper;

        public JuriController(API_MEIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Juri
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var juriList = await _context.Juri.ToListAsync();

            if (juriList == null || juriList.Count == 0)
            {
                return NotFound("Nenhum júri encontrado.");
            }

            return Ok(juriList);
        }

        // GET: Juri/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJuriById(int id)
        {
            var juri = await _context.Juri.FindAsync(id);

            if (juri == null)
            {
                return NotFound($"Júri com ID {id} não encontrado.");
            }

            return Ok(juri);
        }

        // POST: Juri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Juri input)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(input);
                    await _context.SaveChangesAsync();

                    // Return the created object's ID
                    return Ok(new { Id = input.Id, Message = "Juri criado com sucesso!" });
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao criar Juri: {ex.Message}");
                }
            }
            else
            {
                // retorna uma mensagem de erro com os detalhes do modelo inválido
                return BadRequest($"Erro ao criar júri: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Juri juri)
        {
            var existingJuri = await _context.Juri.FindAsync(id);
            if (existingJuri == null)
            {
                return NotFound($"Juri com ID {id} não encontrado.");
            }

            try
            {

                _context.Entry(existingJuri).State = EntityState.Detached;

                _context.Update(juri);

                await _context.SaveChangesAsync();

                return Ok(juri);
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
            if (_context.Juri == null)
            {
                return Problem("Entity set 'API_MEIContext.Juris' is null.");
            }
            var juri = await _context.Juri.FindAsync(id);

            if (juri != null)
            {
                _context.Juri.Remove(juri);
                await _context.SaveChangesAsync();
                return Ok("Eliminado com sucesso");
            }

            return NotFound("ERRO - Não foi encontrada nenhum Juri");
        }


        [HttpPost("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Nenhum ID de juri foi fornecido para exclusão múltipla.");
            }

            var jurisNew = await _context.Juri.Where(j => ids.Contains(j.Id)).ToListAsync();

            if (jurisNew == null || jurisNew.Count == 0)
            {
                return NotFound("Nenhum juri encontrado com os IDs fornecidos para exclusão múltipla.");
            }

            try
            {
                _context.Juri.RemoveRange(jurisNew);
                await _context.SaveChangesAsync();
                return Ok($"Exclusão múltipla de {jurisNew.Count} jurisdicionados concluída com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao excluir múltiplos jurisdicionados: {ex.Message}");
            }
        }
    }
}



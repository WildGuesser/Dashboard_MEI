using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_MEI.Data;
using API_MEI.Models;
using AutoMapper;
using API_MEI.DTOs;


namespace API_MEI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EspecialistasController : ControllerBase
    {
        private readonly API_MEIContext _context;
        private readonly IMapper _mapper;

        public EspecialistasController(API_MEIContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: Especialistas
        [HttpGet]
        public async Task<IActionResult> GetEspecialistas()
        {
            var especialistas = await _context.Especialistas.Include(e => e.Empresas).ToListAsync();
            var especialistasDto = _mapper.Map<List<EspecialistasDTO>>(especialistas);
            return Ok(especialistasDto);
        }

        // GET: Especialistas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEspecialista(string id)
        {
            var especialista = await _context.Especialistas.Include(e => e.Empresas).FirstOrDefaultAsync(e => e.Id == id);

            if (especialista == null)
            {
                return NotFound();
            }

            var especialistaDto = _mapper.Map<EspecialistasDTO>(especialista);
            return Ok(especialistaDto);
        }

        // POST: Especialistas
        [HttpPost]
        public async Task<IActionResult> CreateEspecialista(EspecialistasDTO especialistaDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var especialista = _mapper.Map<Especialistas>(especialistaDto);
                    _context.Add(especialista);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetEspecialista), new { id = especialista.Id }, _mapper.Map<EspecialistasDTO>(especialista));
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
        public async Task<IActionResult> UpdateEspecialista(string id, EspecialistasDTO especialistaDto)
        {
            if (id != especialistaDto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var especialista = _mapper.Map<Especialistas>(especialistaDto);
                    _context.Update(especialista);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateException ex)
                {
                    return BadRequest($"Erro ao atualizar especialista: {ex.Message}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erro interno: {ex.Message}");
                }
            }
            else
            {
                return BadRequest($"Erro ao atualizar especialista: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }
        // DELETE: api/especialistas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialista(string id)
        {
            var especialista = await _context.Especialistas.FindAsync(id);

            if (especialista == null)
            {
                return NotFound();
            }

            try
            {
                _context.Especialistas.Remove(especialista);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Erro ao excluir especialista: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
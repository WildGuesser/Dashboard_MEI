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
        [HttpGet]
        public async Task<IActionResult> GetDocentes()
        {
            var docentes = await _context.Docentes.ToListAsync();
            var docentesDTO = _mapper.Map<List<DocentesDTO>>(docentes);
            return Ok(docentesDTO);
        }

        // GET: Docentes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocente(string id)
        {
            var docente = await _context.Docentes.FindAsync(id);

            if (docente == null)
            {
                return NotFound();
            }

            var docenteDTO = _mapper.Map<DocentesDTO>(docente);
            return Ok(docenteDTO);
        }

        // POST: Docentes
        [HttpPost]
        public async Task<IActionResult> CreateDocente(DocentesDTO docenteDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var docente = _mapper.Map<Docentes>(docenteDTO);
                    _context.Add(docente);
                    await _context.SaveChangesAsync();
                    var createdDocenteDTO = _mapper.Map<DocentesDTO>(docente);
                    return CreatedAtAction(nameof(GetDocente), new { id = createdDocenteDTO.Id }, createdDocenteDTO);
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
        public async Task<IActionResult> UpdateDocente(string id, DocentesDTO docenteDTO)
        {
            if (id != docenteDTO.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var docente = _mapper.Map<Docentes>(docenteDTO);
                    _context.Update(docente);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateException ex)
                {
                    return BadRequest($"Erro ao atualizar docente: {ex.Message}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erro interno: {ex.Message}");
                }
            }
            else
            {
                return BadRequest($"Erro ao atualizar docente: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        // DELETE: Docentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocente(string id)
        {
            var docente = await _context.Docentes.FindAsync(id);

            if (docente == null)
            {
                return NotFound();
            }

            try
            {
                _context.Docentes.Remove(docente);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Erro ao excluir docente: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
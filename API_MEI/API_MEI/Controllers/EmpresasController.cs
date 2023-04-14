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
    public class EmpresasController : ControllerBase
    {
        private readonly API_MEIContext _context;
        private readonly IMapper _mapper;

        public EmpresasController(API_MEIContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: Empresas
        [HttpGet]
        public async Task<IActionResult> GetEmpresas()
        {
            var empresas = await _context.Empresas.ToListAsync();
            return Ok(empresas);
        }

        // GET: Empresas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        // POST: Empresas
        [HttpPost]
        public async Task<IActionResult> CreateEmpresa(Empresas empresa)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(empresa);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, empresa);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao criar empresa: {ex.Message}");
                }
            }
            else
            {
                return BadRequest($"Erro ao criar empresa: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        // PUT: Empresas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpresa(int id, Empresas Empresa)
        {

            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            _mapper.Map(Empresa, empresa);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateException ex)
                {
                    return BadRequest($"Erro ao atualizar empresa: {ex.Message}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erro interno: {ex.Message}");
                }
            }
            else
            {
                return BadRequest($"Erro ao atualizar empresa: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }
        // DELETE: Empresas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            try
            {
                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Erro ao excluir empresa: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
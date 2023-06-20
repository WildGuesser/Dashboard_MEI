using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API_MEI.Data;
using API_MEI.Models;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using API_MEI.DTOs;
using Microsoft.AspNetCore.Authorization;


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
            _context = context;
            _mapper = mapper;
        }

        // GET: Empresas
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var empresas = await _context.Empresas
                .Include(j => j.Especialistas).ThenInclude(jm => jm.Membros).ToListAsync(); 

            return Ok(empresas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresaById(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return NotFound($"Empresa com ID {id} não encontrado.");
            }

            var empresaDTO = _mapper.Map<EmpresasDTO>(empresa);
            return Ok(empresaDTO);
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        public async Task<IActionResult> Create(EmpresasDTO empresasDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var empresa = _mapper.Map<Empresas>(empresasDTO);
                    _context.Add(empresa);
                    await _context.SaveChangesAsync();
                    return Ok("Empresa criado com sucesso!"); // retorna uma mensagem de sucesso
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao criar empresa: {ex.Message}"); // retorna uma mensagem de erro com a exceção
                }
            }
            else
            {
                // retorna uma mensagem de erro com os detalhes do modelo inválido
                return BadRequest($"Erro ao criar empresa: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Empresas empresasDTO)
        {
            var existingEmpresa = await _context.Empresas.FindAsync(id);
            if (existingEmpresa == null)
            {
                return NotFound($"Empresa com ID {id} não encontrado.");
            }

            try
            {
                _context.Entry(existingEmpresa).State = EntityState.Detached;
                _context.Update(empresasDTO);
                await _context.SaveChangesAsync();

                return Ok(empresasDTO);
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
            var empresas = await _context.Empresas.FindAsync(id);
            if (empresas == null)
            {
                return NotFound("Empresa não encontrado.");
            }

            try
            {
                _context.Empresas.Remove(empresas);
                await _context.SaveChangesAsync();
                return Ok("Empresa excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao excluir o empresa: {ex.Message}");
            }
        }

        [HttpPost("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Nenhum ID de empresa foi fornecido para exclusão múltipla.");
            }

            var empresasToDelete = await _context.Empresas.Where(a => ids.Contains(a.Id)).ToListAsync();

            if (empresasToDelete == null || empresasToDelete.Count == 0)
            {
                return NotFound("Nenhum empresa encontrado com os IDs fornecidos para exclusão múltipla.");
            }

            try
            {
                _context.Empresas.RemoveRange(empresasToDelete);
                await _context.SaveChangesAsync();
                return Ok($"Exclusão múltipla de {empresasToDelete.Count} empresas concluída com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao excluir múltiplos empresas: {ex.Message}");
            }
        }

    }
}

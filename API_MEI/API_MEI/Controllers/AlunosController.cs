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

namespace API_MEI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly API_MEIContext _context;
        private readonly IMapper _mapper;

        public AlunosController(API_MEIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Alunos
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var alunos = await _context.Alunos.ToListAsync();
            var alunosDTO = _mapper.Map<List<AlunosDTO>>(alunos);
            return Ok(alunosDTO);
        }

        // GET: Alunos
        [HttpGet("List_Active")]
        public async Task<IActionResult> List_Active()
        {
            var alunos = await _context.Alunos
                .Where(a => a.Estado == true && !_context.Trabalhos.Any(t => t.Aluno_Id == a.Id))
                .ToListAsync();
            var alunosDTO = _mapper.Map<List<AlunosDTO>>(alunos);
            return Ok(alunosDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlunoById(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound($"Aluno com ID {id} não encontrado.");
            }

            var alunoDTO = _mapper.Map<AlunosDTO>(aluno);
            return Ok(alunoDTO);
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        public async Task<IActionResult> Create(AlunosDTO alunosDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var aluno = _mapper.Map<Alunos>(alunosDTO);
                    _context.Add(aluno);
                    await _context.SaveChangesAsync();
                    return Ok("Aluno criado com sucesso!"); // retorna uma mensagem de sucesso
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao criar aluno: {ex.Message}"); // retorna uma mensagem de erro com a exceção
                }
            }
            else
            {
                // retorna uma mensagem de erro com os detalhes do modelo inválido
                return BadRequest($"Erro ao criar aluno: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, AlunosDTO alunosDTO)
        {
            var existingStudent = await _context.Alunos.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound($"Aluno com ID {id} não encontrado.");
            }

            try
            {
                if (existingStudent.Numero_Aluno == alunosDTO.Numero_Aluno && existingStudent.Id != id)
                {
                    return BadRequest($"O número do aluno '{alunosDTO.Numero_Aluno}' já está em uso.");
                }

                existingStudent.Nome = alunosDTO.Nome;
                existingStudent.Numero_Aluno = alunosDTO.Numero_Aluno;
                existingStudent.Email = alunosDTO.Email;

                _context.Update(existingStudent);
                await _context.SaveChangesAsync();

                return Ok(existingStudent);
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
            var alunos = await _context.Alunos.FindAsync(id);
            if (alunos == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            try
            {
                _context.Alunos.Remove(alunos);
                await _context.SaveChangesAsync();
                return Ok("Aluno excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao excluir o aluno: {ex.Message}");
            }
        }

        [HttpPost("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Nenhum ID de aluno foi fornecido para exclusão múltipla.");
            }

            var alunosToDelete = await _context.Alunos.Where(a => ids.Contains(a.Id)).ToListAsync();

            if (alunosToDelete == null || alunosToDelete.Count == 0)
            {
                return NotFound("Nenhum aluno encontrado com os IDs fornecidos para exclusão múltipla.");
            }

            try
            {
                _context.Alunos.RemoveRange(alunosToDelete);
                await _context.SaveChangesAsync();
                return Ok($"Exclusão múltipla de {alunosToDelete.Count} alunos concluída com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao excluir múltiplos alunos: {ex.Message}");
            }
        }

    }
}

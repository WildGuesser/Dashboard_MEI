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
    public class AlunosController : Controller
    {
        private readonly API_MEIContext _context;
        private readonly IMapper _mapper;

        public AlunosController(API_MEIContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: Alunos
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var aPI_MEIContext = _context.Alunos;
            return Ok(await aPI_MEIContext.ToListAsync());
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     
        public async Task<IActionResult> Create(AlunosDTO alunosDTO)
        {
            var alunos = _mapper.Map<Alunos>(alunosDTO);    

            if (ModelState.IsValid) 
            {

                try
                {
                    _context.Add(alunos);
                    await _context.SaveChangesAsync();
                    return Ok("Aluno criado com sucesso!"); // retorna uma mensagem de sucesso
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao criar atividade: {ex.Message}"); // retorna uma mensagem de erro com a exceção
                }
            }
            else
            {
                // retorna uma mensagem de erro com os detalhes do modelo inválido
                return BadRequest($"Erro ao criar atividade: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
            }
        }




        [HttpPut]
        public async Task<IActionResult> Edit(AlunosDTO alunosDTO)
        {
            // Aqui estamos mapeando a trabalhoDTO para uma instância de trabalho
            var alunos = _mapper.Map<Alunos>(alunosDTO);

            try
            {
                // Aqui estamos adicionando a atividade ao contexto e salvando as alterações no banco de dados
                _context.Update(alunos);
                await _context.SaveChangesAsync();

                // Se tudo correr bem, retornamos um Ok com a trabalhoDTO
                return Ok("Aluno alterado com sucesso");
            }
            catch (DbUpdateException ex)
            {
                // Se houver um erro ao salvar no banco de dados, retornamos um BadRequest com a mensagem de erro
                return BadRequest($"Erro ao salvar no banco de dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Se houver qualquer outro tipo de erro, retornamos um StatusCode 500 com a mensagem de erro
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Alunos == null)
            {
                return Problem("Entity set 'API_MEIContext.Alunos'  is null.");
            }
            var alunos = await _context.Alunos.FindAsync(id);

            if (alunos != null)
            {
                _context.Alunos.Remove(alunos);
                await _context.SaveChangesAsync();
                return Ok("Elimiado com sucesso");
            }

            return NotFound("ERRO - Nao foi encontrada nenhum Aluno");
        }


    }
}

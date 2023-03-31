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
using API_MEI.DTOs;

namespace API_MEI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrabalhoesController : Controller
    {

        private readonly API_MEIContext _context;
        private readonly IMapper _mapper;
        public TrabalhoesController(API_MEIContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("TodosTrabalhos")]
        // GET: Trabalhoes
        public async Task<IActionResult> Index()
        {
            var aPI_MEIContext = _context.Trabalho.Include(t => t.Alunos).Include(t => t.Equipa_Orientadores).Include(t => t.Juri).Include(t =>t.Trabalho_Empresa);
            return Ok(await aPI_MEIContext.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Create(TrabalhoDTO trabalhoDTO)  
        {
            var trabalho = _mapper.Map<Trabalho>(trabalhoDTO);    //coloca na variavel trabalho apenas as variaveis que queremos colocar na criaçao de um novo trabalho, por json.

            if (ModelState.IsValid) // Verifica se o modelo é válido
            {

                try
                {
                    _context.Add(trabalho);
                    await _context.SaveChangesAsync();
                    return Ok("Atividade criada com sucesso!"); // retorna uma mensagem de sucesso
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
        public async Task<IActionResult> Edit(TrabalhoDTOwID trabalhoDTOwID)
        {
            // Aqui estamos mapeando a trabalhoDTO para uma instância de trabalho
            var trabalho = _mapper.Map<Trabalho>(trabalhoDTOwID);

            try
            {
                // Aqui estamos adicionando a atividade ao contexto e salvando as alterações no banco de dados
                _context.Update(trabalho);
                await _context.SaveChangesAsync();

                // Se tudo correr bem, retornamos um Ok com a trabalhoDTO
                return Ok("Trabalho alterado com sucesso");
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

                if (_context.Trabalho == null)
                {
                    return Problem("Entity set 'API_DashboardContext.Atividade'  is null.");
                }
                var atividade = await _context.Trabalho.FindAsync(id);
                if (atividade != null)
                {
                    _context.Trabalho.Remove(atividade);
                    await _context.SaveChangesAsync();
                    return Ok("Eliminado com sucesso");
                }


                return NotFound("ERRO - Nao foi encontrada atividade");
            }




    }
}

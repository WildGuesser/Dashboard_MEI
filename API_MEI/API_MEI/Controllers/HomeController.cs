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
    public class HomeController : ControllerBase
    {
        private readonly API_MEIContext _context;
        private readonly IMapper _mapper;

        public HomeController(API_MEIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Home
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var homeData = new Calcs();

                // Get the number of Alunos
                homeData.Nalunos = await _context.Alunos.CountAsync();

                // Get the number of Trabalhos
                homeData.NTrabalhos = await _context.Trabalhos.CountAsync();

                // Get the number of Membros
                homeData.Nmenbros = await _context.Membros.CountAsync();

                // Get the nearest date
                homeData.DataMaisProxima = await _context.Trabalhos
                    .Where(t => t.Juri.Data_Defesa >= DateTime.Today) // Filter for dates greater than or equal to today
                    .OrderBy(t => t.Juri.Data_Defesa)
                    .Select(t => t.Juri.Data_Defesa)
                    .FirstOrDefaultAsync();

                return Ok(homeData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error while retrieving home data: {ex.Message}");
            }

        }
    }
}

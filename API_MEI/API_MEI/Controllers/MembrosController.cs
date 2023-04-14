//using API_MEI.Data;
//using API_MEI.Models;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace API_MEI.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class MembrosController : ControllerBase
//    {
//        private readonly API_MEIContext _context;
//        private readonly IMapper _mapper;

//        public MembrosController(API_MEIContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        // GET: api/Membros
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<MembrosDTO>>> GetMembros()
//        {
//            var membros = await _context.Membros
//                .Include(m => m.Equipa_Orientadores)
//                .Include(m => m.Docentes)
//                .ToListAsync();

//            return _mapper.Map<List<MembrosDTO>>(membros);
//        }

//        // GET: api/Membros/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<MembrosDTO>> GetMembros(int id)
//        {
//            var membros = await _context.Membros
//                .Include(m => m.Equipa_Orientadores)
//                .Include(m => m.Docentes)
//                .FirstOrDefaultAsync(m => m.Equipa_Orientadores_Id == id);

//            if (membros == null)
//            {
//                return NotFound();
//            }

//            return _mapper.Map<MembrosDTO>(membros);
//        }

//        // PUT: api/Membros/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutMembros(int id, MembrosDTO membrosDto)
//        {
//            if (id != membrosDto.Id)
//            {
//                return BadRequest();
//            }

//            var membros = await _context.Membros.FindAsync(id);

//            if (membros == null)
//            {
//                return NotFound();
//            }

//            _mapper.Map(membrosDto, membros);

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(membros);
//                    await _context.SaveChangesAsync();
//                    return NoContent();
//                }
//                catch (DbUpdateException ex)
//                {
//                    return BadRequest($"Erro ao atualizar membros: {ex.Message}");
//                }
//                catch (Exception ex)
//                {
//                    return StatusCode(500, $"Erro interno: {ex.Message}");
//                }
//            }
//            else
//            {
//                return BadRequest($"Erro ao atualizar membros: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
//            }
//        }

//        // POST: api/membros
//        [HttpPost]
//        public async Task<ActionResult<MembrosDTO>> PostMembros(MembrosDTO membrosDTO)
//        {
//            var membros = _mapper.Map<Membros>(membrosDTO);

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Membros.Add(membros);
//                    await _context.SaveChangesAsync();

//                    return CreatedAtAction(nameof(GetMembros), new { id = membros.Equipa_Orientadores_Id }, _mapper.Map<MembrosDTO>(membros));
//                }
//                catch (DbUpdateException ex)
//                {
//                    return BadRequest($"Erro ao criar membros: {ex.Message}");
//                }
//                catch (Exception ex)
//                {
//                    return StatusCode(500, $"Erro interno: {ex.Message}");
//                }
//            }
//            else
//            {
//                return BadRequest($"Erro ao criar membros: Modelo inválido. Erros: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
//            }
//        }

//        // DELETE: api/membros/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteMembros(int id)
//        {
//            var membros = await _context.Membros.FindAsync(id);

//            if (membros == null)
//            {
//                return NotFound();
//            }

//            try
//            {
//                _context.Membros.Remove(membros);
//                await _context.SaveChangesAsync();
//                return NoContent();
//            }
//            catch (DbUpdateException ex)
//            {
//                return BadRequest($"Erro ao remover membros: {ex.Message}");
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Erro interno: {ex.Message}");
//            }
//        }

//    }
//}
using API_MEI.DTOs;
using API_MEI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using API_MEI.Data;

namespace API_MEI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JuriController : ControllerBase
    {
        private readonly API_MEIContext _context;
        private readonly IMapper _mapper;        
        

        public JuriController(API_MEIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Retorna todos os Juri 
        [HttpGet]
        public ActionResult<IEnumerable<JuriDTO>> GetAllJuri()
        {
            var juri = _context.Juri.ToList();
            var juriDTOs = _mapper.Map<List<JuriDTO>>(juri);
            return Ok(juriDTOs);
        }

        // Retorna um Juri específico pelo Id
        [HttpGet("{id}")]
        public ActionResult<JuriDTO> GetJuriById(int id)
        {
            var juri = _context.Juri.FirstOrDefault(j => j.Id == id);

            if (juri == null)
            {
                return NotFound();
            }

            var juriDTO = _mapper.Map<JuriDTO>(juri);
            return Ok(juriDTO);
        }

        // Cria um novo Juri
        [HttpPost]
        public ActionResult<JuriDTO> CreateJuri(JuriDTO juriDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var juri = _mapper.Map<Juri>(juriDTO);

            _context.Juri.Add(juri);
            _context.SaveChanges();

            var juriDTOCreated = _mapper.Map<JuriDTO>(juri);

            return CreatedAtAction(nameof(GetJuriById), new { id = juriDTOCreated.Id }, juriDTOCreated);
        }

        [HttpPut("{id}")]
        public ActionResult<JuriDTO> UpdateJuri(int id, JuriDTO juriDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var juri = _context.Juri.FirstOrDefault(j => j.Id == id);

            if (juri == null)
            {
                return NotFound();
            }

            _mapper.Map(juriDTO, juri);

            _context.SaveChanges();

            return Ok(juriDTO);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteJuri(int id)
        {
            var juri = _context.Juri.FirstOrDefault(j => j.Id == id);

            if (juri == null)
            {
                return NotFound();
            }

            _context.Juri.Remove(juri);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
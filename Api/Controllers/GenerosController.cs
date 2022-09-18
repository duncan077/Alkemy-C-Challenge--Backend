using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisneyApi.Data;
using Microsoft.AspNetCore.Authorization;
using DisneyApi.Model.Genero;
using AutoMapper;

namespace DisneyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly DisneyContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GenerosController> _logger;

        public GenerosController(DisneyContext context, IMapper mapper, ILogger<GenerosController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Generos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneroDTO>>> Getgeneros()
        {
            return _mapper.Map<List<Genero>,List<GeneroDTO>>(await _context.generos.ToListAsync());
        }

        // GET: api/Generos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneroDTO>> GetGenero(int id)
        {
            var genero = await _context.generos.FindAsync(id);

            if (genero == null)
            {
                return NotFound();
            }

            return _mapper.Map<Genero,GeneroDTO>(genero);
        }

        // PUT: api/Generos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutGenero(int id, GeneroDTO genero)
        {
            var genro = await _context.generos.FindAsync(id);
            if(genro == null)
            {
                return BadRequest();
            }
            genro.Imagen = genero.Imagen;
            genro.Nombre = genero.Nombre;
            _context.Entry(genero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Generos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GeneroDTO>> PostGenero(GeneroDTO genero)
        {
            _context.generos.Add(_mapper.Map<GeneroDTO,Genero>(genero));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenero", new { id = genero.Id }, genero);
        }

        // DELETE: api/Generos/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            var genero = await _context.generos.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }

            _context.generos.Remove(genero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeneroExists(int id)
        {
            return _context.generos.Any(e => e.Id == id);
        }
    }
}

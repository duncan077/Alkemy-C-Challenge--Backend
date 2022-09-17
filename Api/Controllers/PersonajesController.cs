using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisneyApi.Data;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DisneyApi.Model.Personaje;
using DisneyApi.Model.Pelicula;

namespace DisneyApi.Controllers
{
    [Route("api/characters")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private readonly DisneyContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PersonajesController(DisneyContext context, IMapper mapper, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonajeSimpleDTO>>> Getpersonajes([FromQuery] PjParameters pjParameters)
        {
            var response = _mapper.Map<List<Personaje>, List<PersonajeSimpleDTO>>(
                await _context.personajes.Where(
                    p=>p.Edad.Equals(pjParameters.age)
                    ||p.Nombre.Equals(pjParameters.name)
                    ||p.peliculas.Any(p=>p.Id.Equals(pjParameters.movies)))
                .ToListAsync<Personaje>());
            return  response;

        }

        // GET: api/characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonajeFullDTO>> GetPersonaje(int id)
        {
            var personaje = await _context.personajes.FindAsync(id);

            if (personaje == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<Personaje, PersonajeFullDTO>(personaje);
            return response;
        }

        // PUT: api/characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPersonaje(int id, PersonajeModelDTO personaje)
        {
            var pj = _mapper.Map<PersonajeModelDTO, Personaje>(personaje);
            pj.Id = id;
            AddPelicula(personaje,pj);
            _context.Entry(pj).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PersonajeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Error on put personaje, {ex.Message}");
                    return StatusCode(500, "Unexpected Error, please try again");
                }
            }

            return NoContent();
        }

        // POST: api/characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Personaje>> PostPersonaje(PersonajeModelDTO personaje)
        {
            try
            {
                var pj = _mapper.Map<PersonajeModelDTO, Personaje>(personaje);
                AddPelicula(personaje, pj);
                            _context.personajes.Add(pj);
                            await _context.SaveChangesAsync();

                            return CreatedAtAction("GetCharacter", new { id = pj.Id }, personaje);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on post personaje, {ex.Message}");
                return StatusCode(500, "Unexpected Error, please try again");
            }
            
        }

        // DELETE: api/characters/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePersonaje(int id)
        {
            var personaje = await _context.personajes.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }

            _context.personajes.Remove(personaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonajeExists(int id)
        {
            return _context.personajes.Any(e => e.Id == id);
        }
        private void AddPelicula(PersonajeModelDTO personaje, Personaje result)
        {
            foreach (var item in personaje.peliculas)
            {
                if (_context.peliculas.Any(g => g.Id == item))
                    result.peliculas.Add(_context.peliculas.Find(item));
                _logger.LogWarning($"Pelicula {item} no existe");
            }
         
        }
    }
    public class PjParameters
    {
        public string? name { get; set; }
        public int? age { get; set; }
        public int? movies {get; set; }
    }
}

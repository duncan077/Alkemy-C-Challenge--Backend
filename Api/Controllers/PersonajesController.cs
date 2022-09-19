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
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;

namespace DisneyApi.Controllers
{
    [Route("api/characters")]
    [ApiController]
    [DisableCors]
    public class PersonajesController : ControllerBase
    {
        private readonly DisneyContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonajesController> _logger;

        public PersonajesController(DisneyContext context, IMapper mapper, ILogger<PersonajesController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonajeSimpleDTO>>> Getpersonajes([FromQuery] PjParameters pjParameters)
        {
            var result = 
                await _context.personajes.Include(p=>p.peliculas).ToListAsync<Personaje>();

            if(!pjParameters.name.IsNullOrEmpty())
            {
                result=result.Where(
                    p => p.Nombre.Equals(pjParameters.name)).ToList();
            }
            if (pjParameters.age!=-1)
            {
                 result=result.Where(
                     p => p.Edad==(pjParameters.age)).ToList();
            }
            if (pjParameters.movies!=-1)
            {
                result= result.Where(
                     p => p.peliculas.Any(pe=>pe.Id==pjParameters.movies)).ToList();
            }
            return _mapper.Map<List<Personaje>, List<PersonajeSimpleDTO>>(result);

        }

        // GET: api/characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonajeFullDTO>> GetPersonaje(int id)
        {
            var personaje = await _context.personajes.Include(p=>p.peliculas).FirstAsync(p=>p.Id==id);

            if (personaje == null)
            {
                return NotFound();
            }
             
            return _mapper.Map<Personaje, PersonajeFullDTO>(personaje);
        }

        // PUT: api/characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPersonaje(int id, PersonajeModelDTO personaje)
        {
            var pj = _mapper.Map<PersonajeModelDTO, Personaje>(personaje);
            pj.Id = id;
            pj.peliculas.Clear();
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
        public async Task<ActionResult<PersonajeModelDTO>> PostPersonaje(PersonajeModelDTO personaje)
        {
            try
            {
                var pj = _mapper.Map<PersonajeModelDTO, Personaje>(personaje);
                pj.peliculas.Clear();
                AddPelicula(personaje, pj);
                            _context.personajes.Add(pj);
                            await _context.SaveChangesAsync();

                            return CreatedAtAction("GetPersonaje", new { id = pj.Id }, personaje);
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
            if(!personaje.peliculas.IsNullOrEmpty())
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
        private int? _age { get; set; } = -1;
        private int? _movies { get; set; } = -1;

        public int? age
        {
            get
            {
                return _age;
            }
            set
            {
                if (value.HasValue)
                    if (value.Value >= -1)
                        _age = value;
            }
        }
        public int? movies
        {
            get
            {
                return _movies;
            }
            set
            {
                if (value.HasValue)
                    if (value.Value >= -1)
                        _movies = value;
            }
        }
    }
}

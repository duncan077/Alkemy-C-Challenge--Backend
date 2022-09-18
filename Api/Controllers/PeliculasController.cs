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
using DisneyApi.Model.Pelicula;
using Microsoft.IdentityModel.Tokens;
using DisneyApi.Model.Genero;
using DisneyApi.Model.Personaje;

namespace DisneyApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly DisneyContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PeliculasController> _logger;

        public PeliculasController(DisneyContext context, IMapper mapper, ILogger<PeliculasController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeliculaSimpleDTO>>> Getpeliculas([FromQuery] PeliculasParameter peliculasParameter)
        {
            var result =
                await _context.peliculas.Include(p=>p.personajes).Include(p=>p.generos).ToListAsync<Pelicula>();

            if (!peliculasParameter.name.IsNullOrEmpty())
            {
                result = result.Where(
                    p => p.Titulo.Equals(peliculasParameter.name)).ToList();
            }
            if (peliculasParameter.genre != -1)
            {
                result = result.Where(
                    p => p.generos.Any(g=>g.Id== peliculasParameter.genre)).ToList();
            }
           
            
            
            if (peliculasParameter.order == "ASC")
                result.OrderBy(o => o.fechaCreacion);
            else
                result.OrderByDescending(o => o.fechaCreacion);
           

            return  _mapper.Map<List<Pelicula>, List<PeliculaSimpleDTO>>(result);
        }

        // GET: api/movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PeliculaFullDTO>> GetPelicula(int id)
        {
            var pelicula = await _context.peliculas.Include(p => p.generos).Include(p => p.personajes).FirstAsync(p=>p.Id==id);

            if (pelicula == null)
            {
                return NotFound();
            }
           
            
            return _mapper.Map<Pelicula,PeliculaFullDTO>(pelicula);
        }

        // PUT: api/movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPelicula(int id, PeliculaModelDTO pelicula)
        {
            var result = _mapper.Map<PeliculaModelDTO, Pelicula>(pelicula);
            result.Id = id;
            pelicula.personajes.Clear();
            pelicula.generos.Clear();
            AddPjAndGenro(pelicula, result);

            _context.Entry(result).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PeliculaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Error on put movies, {ex.Message}");
                    return StatusCode(500, "Unexpected Error, please try again");
                }
            }

            return NoContent();
        }

       

        // POST: api/movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PeliculaModelDTO>> PostPelicula(PeliculaModelDTO peliculaDto)
        {
            
            try
            {
                var pelicula = _mapper.Map<PeliculaModelDTO, Pelicula>(peliculaDto);
               
                pelicula.personajes.Clear();
               
                    pelicula.generos.Clear();
                AddPjAndGenro(peliculaDto, pelicula);
                _context.peliculas.Add(pelicula);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPelicula", new { id = pelicula.Id }, peliculaDto);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error on post peliculas, {ex.Message}");
                return StatusCode(500, "Unexpected Error, please try again");
            }
           
        }

        // DELETE: api/movies/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePelicula(int id)
        {
            var pelicula = await _context.peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            _context.peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeliculaExists(int id)
        {
            return _context.peliculas.Any(e => e.Id == id);
        }
        private void AddPjAndGenro(PeliculaModelDTO pelicula, Pelicula result)
        {
            if (!pelicula.personajes.IsNullOrEmpty())
                foreach (var item in pelicula.personajes)
            {
                if (_context.personajes.Any(g => g.Id == item))
                    result.personajes.Add( _context.personajes.Find(item));
                else
                _logger.LogWarning($"Personaje {item} no existe");
            }
            if (!pelicula.generos.IsNullOrEmpty())
                foreach (var item in pelicula.generos)
            {
                if (_context.generos.Any(g => g.Id == item))
                    result.generos.Add(_context.generos.Find(item));
                else
                    _logger.LogWarning($"Genero {item} no existe");
            }
        }
    }

    public class PeliculasParameter
        {
       

        public string? name { get; set; }
        private int? _genre { get; set; } = -1;

        private string? _order ="ASC";
        public string? order
        {
            get
            {
                return _order;
            }
            set
            {
                if (value =="ASC"||value=="DESC")
                    _order = value;
            }
        }
        public int? genre
        {
            get
            {
                return _genre;
            }
            set
            {
                if (value.HasValue)
                    if(value.Value>=-1)
                    _genre = value;
            }
        }

    }
}

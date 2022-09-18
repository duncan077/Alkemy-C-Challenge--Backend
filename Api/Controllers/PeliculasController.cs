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

namespace DisneyApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly DisneyContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PeliculasController(DisneyContext context, IMapper mapper, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeliculaSimpleDTO>>> Getpeliculas([FromQuery] PeliculasParameter peliculasParameter)
        {
            var result= await _context.peliculas.Where(p=>p.Titulo.Equals(peliculasParameter.name)||p.generos.Any(g=>g.Id.Equals(peliculasParameter.genre))).ToListAsync();
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
            var pelicula = await _context.peliculas.FindAsync(id);

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
        public async Task<ActionResult<Pelicula>> PostPelicula(PeliculaModelDTO peliculaDto)
        {
            
            try
            {
                var pelicula = _mapper.Map<PeliculaModelDTO, Pelicula>(peliculaDto);
                AddPjAndGenro(peliculaDto, pelicula);
                _context.peliculas.Add(pelicula);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPelicula", new { id = pelicula.Id }, pelicula);
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
            foreach (var item in pelicula.personajes)
            {
                if (_context.personajes.Any(g => g.Id == item))
                    result.personajes.Add( _context.personajes.Find(item));
                _logger.LogWarning($"Personaje {item} no existe");
            }
            foreach (var item in pelicula.generos)
            {
                if (_context.generos.Any(g => g.Id == item))
                    result.generos.Add(_context.generos.Find(item));
                _logger.LogWarning($"Genero {item} no existe");
            }
        }
    }

    public class PeliculasParameter
        {
        public PeliculasParameter(string? order)
        {
            this.order = order;
        }

        public string? name { get; set; }
        public int? genre { get; set; }
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

    }
}

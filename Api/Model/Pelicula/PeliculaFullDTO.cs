using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DisneyApi.Model.Personaje;
using DisneyApi.Model.Genero;

namespace DisneyApi.Model.Pelicula
{
    public class PeliculaFullDTO
    {
       
        public int Id { get; set; }
        
        public string Titulo { get; set; }
      
        public string Imagen { get; set; }

        [Range(1, 5, ErrorMessage = "Please enter valid integer Number")]
        public int Calificacion { get; set; } = 1;
     
        public DateTime fechaCreacion { get; set; }

        public  List<PersonajeSimpleDTO>? personajes { get; set; } = new List<PersonajeSimpleDTO>();
        public List<GeneroDTO>? generos { get; set; }=  new List<GeneroDTO>();
    }
}

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

      
        public int Calificacion { get; set; }
     
        public DateTime fechaCreacion { get; set; }

        public  List<PersonajeSimpleDTO>? personajes { get; set; } = new List<PersonajeSimpleDTO>();
        public List<GeneroDTO>? generos { get; set; }=  new List<GeneroDTO>();
    }
}

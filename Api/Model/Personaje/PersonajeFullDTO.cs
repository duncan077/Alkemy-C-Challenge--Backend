using DisneyApi.Data;
using DisneyApi.Model.Pelicula;
using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Model.Personaje
{
    public class PersonajeFullDTO
    {
       
        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public float? Peso { get; set; }
       
        public string? Historia { get; set; }
        
    
        public string Imagen { get; set; }
        public  List<PeliculaSimpleDTO> peliculas { get; set; }
    }
}

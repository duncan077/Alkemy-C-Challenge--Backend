using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Model.Pelicula
{
    public class PeliculaModelDTO
    {
        
        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; }
        [Required]
    
        public string Imagen { get; set; }

        [Required,Range(1, 5, ErrorMessage = "Please enter valid integer Number")]
        public int Calificacion { get; set; } = 1;
        [Required]
        public DateTime? fechaCreacion { get; set; }

        public  List<int>? personajes { get; set; } = new List<int>();
        public List<int>? generos { get; set; } = new List<int>();
    }
}

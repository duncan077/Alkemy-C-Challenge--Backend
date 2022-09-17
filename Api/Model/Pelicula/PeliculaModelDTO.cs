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
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }

        [Required]
        [MinLength(1), MaxLength(5)]
        public int Calificacion { get; set; }
        [Required]
        public DateTime fechaCreacion { get; set; }

        public  List<int>? personajes { get; set; }
        public List<int>? generos { get; set; }
    }
}

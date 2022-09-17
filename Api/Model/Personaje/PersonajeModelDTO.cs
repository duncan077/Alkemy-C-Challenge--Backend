using DisneyApi.Data;
using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Model.Personaje
{
    public class PersonajeModelDTO
    {
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public float? Peso { get; set; }
        [MaxLength(255)]
        public string? Historia { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
        public List<int>? peliculas { get; set; }
    }
}

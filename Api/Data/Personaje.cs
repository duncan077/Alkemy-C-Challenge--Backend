using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisneyApi.Data
{
    public class Personaje
    {
        public Personaje()
        {
            peliculas = new HashSet<Pelicula>(); 
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
        public virtual ICollection<Pelicula> peliculas { get; set; }
    }
}

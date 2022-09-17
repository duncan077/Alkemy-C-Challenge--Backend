using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Data
{
    public class Genero
    {
        public Genero()
        {
            peliculas = new HashSet<Pelicula>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
        
        public virtual ICollection<Pelicula> peliculas { get; set; }
    }
}

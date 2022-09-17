using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace DisneyApi.Data
{
    public class Pelicula
    {
        public Pelicula()
        {
            personajes = new HashSet<Personaje>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }

        [Required]
        [MinLength(1),MaxLength(5)]
        public int Calificacion { get; set; }
        [Required]
        public DateTime fechaCreacion { get; set; }

        public virtual ICollection<Personaje> personajes { get; set; }
            public virtual ICollection<Genero> generos { get; set; }
    }
}

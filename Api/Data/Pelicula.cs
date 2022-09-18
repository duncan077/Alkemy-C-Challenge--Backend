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
            generos = new HashSet<Genero>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; }
        [Required]
        
        public string Imagen { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Please enter valid integer Number")]
        public int Calificacion { get; set; } = 1;
        [Required]
        public DateTime fechaCreacion { get; set; }

        public virtual ICollection<Personaje> personajes { get; set; }
            public virtual ICollection<Genero> generos { get; set; }
    }
}

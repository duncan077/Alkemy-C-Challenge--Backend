using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Model.Pelicula
{
    public class PeliculaSimpleDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public DateTime fechaCreacion { get; set; }

    }
}

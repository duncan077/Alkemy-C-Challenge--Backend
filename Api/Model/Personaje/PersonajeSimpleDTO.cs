using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Model.Personaje
{
    public class PersonajeSimpleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Imagen { get; set; }
     
    }
}

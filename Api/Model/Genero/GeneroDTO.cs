using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Model.Genero
{
    public class GeneroDTO
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
    }
}

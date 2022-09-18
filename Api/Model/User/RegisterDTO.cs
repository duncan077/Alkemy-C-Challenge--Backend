using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DisneyApi.Model.DTO.User
{
    public class RegisterDTO
    {
        [Required]
        [MinLength(5)]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

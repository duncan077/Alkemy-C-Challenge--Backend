using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Model.DTO.User
{
    public class LoginDTO
    {
        [Required]
        [MinLength(5)]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}

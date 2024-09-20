using System.ComponentModel.DataAnnotations;

namespace WebStore.Server.Models.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string Email { set; get; }
        public string Password { set; get; }
    }
}

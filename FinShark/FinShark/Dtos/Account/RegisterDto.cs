using System.ComponentModel.DataAnnotations;

namespace FinShark.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public required string UserName { get; set; }
        [EmailAddress] 
        public required string Email { get; set;}
        [Required] 
        public required string Password { get; set; }
    }
}

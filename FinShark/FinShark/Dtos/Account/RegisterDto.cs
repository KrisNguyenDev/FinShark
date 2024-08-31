using System.ComponentModel.DataAnnotations;

namespace FinShark.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string? UserName { get; set; }
        [EmailAddress] 
        public string? Email { get; set;}
        [Required] 
        public string? Password { get; set; }
    }
}

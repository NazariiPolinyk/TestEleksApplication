using System.ComponentModel.DataAnnotations;

namespace TestEleksApplication.Services.AuthenticationService
{
    public class AuthenticateModel
    {
        [Required]
        [MaxLength(20)]
        public string Login { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Labo_Sannin_API.Models
{
    public class UserLoginForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
            ErrorMessage = "Doit contenir 8 caractère minimum, une majuscule et un chiffre")]
        public string Password { get; set; }
    }
}

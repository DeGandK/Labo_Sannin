using System.ComponentModel.DataAnnotations;

namespace Labo_Sannin_API.Models
{
    public class UserRegisterForm
    {
        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Adresse { get; set; }

        [Required]
        public string Telephone {  get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
            ErrorMessage = "Doit contenir 8 caractère minimum, une majuscule et un chiffre")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Les deux mots de passe doivent correspondre")]
        public string PasswordConfirm { get; set; }
    }
}

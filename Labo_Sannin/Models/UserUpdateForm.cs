using System.ComponentModel.DataAnnotations;

namespace Labo_Sannin_API.Models
{
    public class UserUpdateForm
    {
        [Required]
        public string Adresse { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

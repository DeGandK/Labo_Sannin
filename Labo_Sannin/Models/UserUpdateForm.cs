using System.ComponentModel.DataAnnotations;

namespace Labo_Sannin_API.Models
{
    public class UserUpdateForm
    {
        [Required]
        public string Adresse { get; set; }

        [Required]
        public string Telephone { get; set; }

    }
}

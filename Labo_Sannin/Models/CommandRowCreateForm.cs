using System.ComponentModel.DataAnnotations;

namespace Labo_Sannin_API.Models
{
    public class CommandRowCreateForm
    {
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantite { get; set; }
    }
}

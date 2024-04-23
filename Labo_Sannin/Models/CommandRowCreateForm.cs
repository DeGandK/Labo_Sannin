using Labo_Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Labo_Sannin_API.Models
{
    public class CommandRowCreateForm : Labo_Domain.Models.CommandRow
    {
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantite { get; set; }
    }
}

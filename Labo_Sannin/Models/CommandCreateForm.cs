using Labo_Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Labo_Sannin_API.Models
{
    public class CommandCreateForm
    {
        
        [Required]
        public int UserID { get; set; }
        [Required]
        public bool IsPaid { get; set; }
        [Required]
        public DateTime DateCommande { get; set; }
        [Required]
        public List<CommandRowCreateForm> produitChoisis { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Labo_Sannin_API.Models
{
    public class CommandCreateForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }      
        public string Type { get; set; }      
    }
}

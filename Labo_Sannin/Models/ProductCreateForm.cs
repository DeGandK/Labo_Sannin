using System.ComponentModel.DataAnnotations;

namespace Labo_Sannin_API.Models
{
    public class ProductCreateForm
    {
        [Required]
        public string Nom {  get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Stock {  get; set; }
        [Required]
        public decimal PrixHTVA { get; set; }
        [Required]
        public string Image {  get; set; }
        [Required]
        public int CategorieID { get; set; }


    }
}

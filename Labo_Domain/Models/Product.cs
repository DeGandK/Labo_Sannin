using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_Domain.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal PrixHTVA { get; set; }
        public string Image {  get; set; }
        public int CategorieID { get; set; }
    }
}

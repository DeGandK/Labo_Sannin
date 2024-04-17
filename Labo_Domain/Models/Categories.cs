using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_Domain.Models
{
    public class Categories
    {
        public int CategorieID { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public int TauxTVA { get; set; }
    }
}

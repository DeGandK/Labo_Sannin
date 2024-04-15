using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_Domain.Models
{
    public class Categories
    {
        public int CategoryID { get; set; }
        public int UserID { get; set; }
        public bool IsPaid {  get; set; }
        public DateTime DateCommande { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_Domain.Models
{
    public class Command
    {
        public int CommandID { get; set; }
        public int UserID { get; set; }
        public bool IsPaid {  get; set; }
        public DateTime DateCommande { get; set; }
    }
}

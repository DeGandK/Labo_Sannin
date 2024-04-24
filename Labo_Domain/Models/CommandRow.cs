using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_Domain.Models
{
    public class CommandRow
    {
        //public int LigneCommandID {  get; set; }
        public int CommandID {  get; set; }
        public int ProductID {  get; set; }
        public int Quantite {  get; set; }
    }
}

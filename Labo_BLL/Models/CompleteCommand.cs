using Labo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_BLL.Models
{
    public class CompleteCommand : Command
    {
        public List<CommandRow> CommandRows { get; set; }

    }
}

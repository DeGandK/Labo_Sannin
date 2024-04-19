using Labo_BLL.Interfaces;
using Labo_DAL.Repositories;
using Labo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_BLL.Services
{
    public class CommandRowService : ICommandRowService
    {
        private readonly ICommandRowRepo _commandRowRepo;
        public CommandRowService(ICommandRowRepo commandRowRepo)
        {
            _commandRowRepo = commandRowRepo;
        }

        public void Create(CommandRow cr) 
        {
            _commandRowRepo.Create(cr);
        }

        public List<CommandRow> GetByCommandId(int id)
        {
            return _commandRowRepo.GetByCommandId(id);
        }
    }
}

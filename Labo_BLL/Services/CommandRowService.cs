using Labo_BLL.Interfaces;
using Labo_BLL.Models;
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
        private readonly IProductRepo _productRepo;
        private readonly ICommandRepo _commandRepo;
        public CommandRowService(ICommandRowRepo commandRowRepo, IProductRepo productRepo, ICommandRepo commandRepo)
        {
            _commandRowRepo = commandRowRepo;
            _productRepo = productRepo;
            _commandRepo = commandRepo;
        }
        public void Create(CommandRow c)
        {
            _commandRowRepo.Create(c);
        }
        public List<CommandRow> GetByCommandId(int id)
        {
            return _commandRowRepo.GetByCommandId(id);
        }
    }
}

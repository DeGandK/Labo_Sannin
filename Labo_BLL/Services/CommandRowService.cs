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
        private readonly IProductRepo _productRepo;
        public CommandRowService(ICommandRowRepo commandRowRepo, IProductRepo productRepo)
        {
            _commandRowRepo = commandRowRepo;
            _productRepo = productRepo;
        }

        public void Create(CommandRow cr) 
        {
            // Récuperer le stock
            int stock = _productRepo.GetByCommandId(cr.stock);
            _commandRowRepo.Create(cr);
        }

        public List<CommandRow> GetByCommandId(int id)
        {
            return _commandRowRepo.GetByCommandId(id);
        }
    }
}

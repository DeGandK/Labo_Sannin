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

        public void Create(CompleteCommand cr)
        {
            // Récuperer le stock
            // Récuperer la quantité
            foreach (CommandRow item in cr.CommandRows)
            {
                int stock = _productRepo.GetStock(item.ProductID);
                int quantite = item.Quantite;
                if (stock > quantite)
                {
                    _commandRepo.Create(cr);
                    _commandRowRepo.Create(item);
                }
                else
                {
                    throw new Exception();
                }
            }

        }

        public List<CommandRow> GetByCommandId(int id)
        {
            return _commandRowRepo.GetByCommandId(id);
        }


    }
}

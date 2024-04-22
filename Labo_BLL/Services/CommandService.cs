using Labo_BLL.Interfaces;
using Labo_BLL.Models;
using Labo_DAL.Repositories;
using Labo_Domain.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_BLL.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IProductRepo _productRepo;
        private readonly ICommandRowRepo _commandRowRepo;

        public CommandService(ICommandRepo commandRepo,IProductRepo productRepo,ICommandRowRepo commandRowRepo)
        {
            _commandRepo = commandRepo;
            _productRepo = productRepo;
            _commandRowRepo = commandRowRepo;
        }

        public void Create(CompleteCommand cr)
        {
            int id = _commandRepo.Create(cr);
            foreach (CommandRow item in cr.CommandRows)
            {
                int stock = _productRepo.GetStock(item.ProductID);
                int quantite = item.Quantite;
                if (stock > quantite)
                {
                    item.CommandID = id;
                    _commandRowRepo.Create(item);
                }
                else
                {
                    throw new Exception();
                }
            }

        }
        public List<Command> GetAll()
        {
            return _commandRepo.GetAll();
        }
        public List<Command> GetCommandsByUserID(int UserID)
        {
            return _commandRepo.GetCommandsbyUserID(UserID);
        }
        
    }
}

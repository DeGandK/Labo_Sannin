using Labo_BLL.Interfaces;
using Labo_BLL.Models;
using Labo_DAL.Repositories;
using Labo_DAL.Services;
using Labo_Domain.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        public CommandService(ICommandRepo commandRepo, IProductRepo productRepo, ICommandRowRepo commandRowRepo)
        {
            _commandRepo = commandRepo;
            _productRepo = productRepo;
            _commandRowRepo = commandRowRepo;
        }
        /// <summary>
        /// Crée un commande reprenant tous les produits sélectionnés
        /// </summary>
        /// <param name="cr"></param>
        /// <exception cref="Exception"></exception>
        public void Create(CompleteCommand cr)
        {
            int id = _commandRepo.Create(cr);
            foreach (CommandRow item in cr.CommandRows)
            {
                int ID = item.ProductID;
                int stock = _productRepo.GetStock(ID);
                int quantite = item.Quantite;
                if (stock >= quantite)
                {
                    item.CommandID = id;
                    _commandRowRepo.Create(item);
                }
                else
                {
                    throw new Exception("Stock insuffisant");
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
        public bool IsValid(int CommandId, bool IsPaid)
        {

            if (IsPaid == true)
            {
                _commandRepo.ValiderCommande(CommandId);
            }
            else
            {
                _commandRepo.DeleteCommande(CommandId);
            }
            return IsPaid;
            //IsValid ? (return _commandRepo.ValiderCommande(CommandId)) : (return _commandRepo.DeleteCommande(CommandId));
        }
        public void ValiderCommande(int CommandId)
        {
            _commandRepo.ValiderCommande(CommandId);
        }
        public void DeleteCommande(int CommandId)
        {
            _commandRepo.DeleteCommande(CommandId);

        }
        public bool CheckIsPaid(int CommandId)
        {
            return _commandRepo.CheckIsPaid(CommandId);
        }
        public void StockAchat(int CommandId)
        {
            List<CommandRow> essai = _commandRowRepo.GetByCommandId(CommandId);
            foreach (CommandRow e in essai)
            {
                Product p = _productRepo.GetById(e.ProductID);
                p.ProductID = e.ProductID;
                p.Nom = p.Nom;
                p.Description = p.Description;
                p.Stock -= e.Quantite;
                p.PrixHTVA = p.PrixHTVA;
                p.Image = p.Image;
                p.CategorieID = p.CategorieID;
                _productRepo.Update(p);
            }
        }
    }
}

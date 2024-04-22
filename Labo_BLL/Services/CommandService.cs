using Labo_BLL.Interfaces;
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

        public CommandService(ICommandRepo commandRepo)
        {
            _commandRepo = commandRepo;
        }
        
        public void Creat(Command c)
        {
            _commandRepo.Creat(c);
        }
        public List<Command> GetAll()
        {
            return _commandRepo.GetAll();
        }
        public List<Command> GetCommandsByUserID(int UserID)
        {
            return _commandRepo.GetCommandsbyUserID(UserID);
        }
        public bool IsValid(int CommandId)
        {
            bool reponse = false;
            if (reponse == true)
            {
                _commandRepo.ValiderCommande(CommandId);
            }
            else
            {
               _commandRepo.DeleteCommande(CommandId);
            }
            return reponse;
            //IsValid ? (return _commandRepo.ValiderCommande(CommandId)) : (return _commandRepo.DeleteCommande(CommandId));
        }
        public void  ValiderCommande(int CommandId)
        {
            _commandRepo.ValiderCommande(CommandId);
        }

        public void DeleteCommande(int CommandId)
        {
            _commandRepo.DeleteCommande(CommandId);

        }
    }
}

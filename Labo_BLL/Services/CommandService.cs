using Labo_DAL.Repositories;
using Labo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_BLL.Services
{
    public class CommandService 
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
    }
}

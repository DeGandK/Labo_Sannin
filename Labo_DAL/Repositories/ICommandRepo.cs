using Labo_Domain.Models;

namespace Labo_DAL.Repositories
{
    public interface ICommandRepo
    {
        void Creat(Command cs);
        List<Command> GetAll();
        List<Command> GetCommandsbyUserID(int UserID);
        void ValiderCommande(int CommandId);
        void DeleteCommande(int CommandId);
    }
}
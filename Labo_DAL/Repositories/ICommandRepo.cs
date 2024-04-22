using Labo_Domain.Models;

namespace Labo_DAL.Repositories
{
    public interface ICommandRepo
    {
        int Create(Command cs);
        List<Command> GetAll();
        List<Command> GetCommandsbyUserID(int UserID);
    }
}
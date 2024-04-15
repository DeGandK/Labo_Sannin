using Labo_Domain.Models;

namespace Labo_BLL.Interfaces
{
    public interface ICommandService
    {
        void Creat(Command c);
        List<Command> GetAll();
        List<Command> GetCommandsByUserID(int UserID);
    }
}
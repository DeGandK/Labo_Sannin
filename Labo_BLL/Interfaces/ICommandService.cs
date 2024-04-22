using Labo_BLL.Models;
using Labo_Domain.Models;

namespace Labo_BLL.Interfaces
{
    public interface ICommandService
    {
        void Create(CompleteCommand cr);
        List<Command> GetAll();
        List<Command> GetCommandsByUserID(int UserID);
    }
}
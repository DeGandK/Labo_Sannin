using Labo_BLL.Models;
using Labo_Domain.Models;

namespace Labo_BLL.Interfaces
{
    public interface ICommandRowService
    {
        IEnumerable<CommandRow> GetByCommandId(int id);
        void Create(CommandRow c);
    }
}
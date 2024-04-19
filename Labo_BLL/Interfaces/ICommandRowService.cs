using Labo_Domain.Models;

namespace Labo_BLL.Interfaces
{
    public interface ICommandRowService
    {
        List<CommandRow> GetByCommandId(int id);
        void Create(CommandRow cr);
    }
}
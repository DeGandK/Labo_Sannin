using Labo_Domain.Models;

namespace Labo_BLL.Interfaces
{
    public interface ICommandRowService
    {
        List<CommandRow> GetByCommandId(int id, int productId);
        void Create(CommandRow cr);
    }
}
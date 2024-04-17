using Labo_Domain.Models;

namespace Labo_BLL.Interfaces
{
    public interface ICategoriesService
    {
        IEnumerable<Categories> GetAll();
        Categories GetById(int id);
    }
}
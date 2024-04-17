using Labo_Domain.Models;

namespace Labo_DAL.Repositories
{
    public interface ICategoriesRepo
    {
        List<Categories> GetAll();
        Categories GetById(int id);
    }
}
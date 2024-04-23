using Labo_BLL.Models;
using Labo_Domain.Models;

namespace Labo_BLL.Interfaces
{
    public interface IProductService
    {
        void Create(Product product);
        void Delete(int id);
        IEnumerable<Product> GetAll();
        CompleteProduct GetById(int id);
        void Update(Product product);
        int GetStock(int id);
    }
}
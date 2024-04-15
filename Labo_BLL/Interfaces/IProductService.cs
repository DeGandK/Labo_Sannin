using Labo_Domain.Models;

namespace Labo_BLL.Interfaces
{
    public interface IProductService
    {
        int Create(Product product);
        void Delete(int id);
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Update(Product product);
    }
}
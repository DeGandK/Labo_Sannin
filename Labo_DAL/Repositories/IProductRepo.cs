using Labo_Domain.Models;

namespace Labo_DAL.Repositories
{
    public interface IProductRepo
    {
        int Create(Product product);
        void Delete(int id);
        List<Product> GetAll();
        Product GetById(int id);
        int GetStock(int id);
        void Update(Product product);

       
    }
}
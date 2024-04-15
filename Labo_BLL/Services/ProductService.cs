using Labo_BLL.Interfaces;
using Labo_DAL.Repositories;
using Labo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public int Create(Product product)
        {
            return _productRepo.Create(product);
        }

        public void Delete(int id)
        {
            _productRepo.Delete(id);
        }

        public void Update(Product product)
        {
            _productRepo.Update(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepo.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepo.GetById(id);
        }
    }
}

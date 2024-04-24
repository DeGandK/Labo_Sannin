using Labo_BLL.Interfaces;
using Labo_BLL.Models;
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
        private readonly ICategoriesRepo _categoriesRepo;
        public ProductService(IProductRepo productRepo, ICategoriesRepo categoriesRepo)
        {
            _productRepo = productRepo;
            _categoriesRepo = categoriesRepo;
        }
        public void Create(Product product)
        {
             _productRepo.Create(product);
        }
        public void Delete(int id)
        {
            _productRepo.Delete(id);
        }
        public void Update(Product product)
        {
            
            _productRepo.Update(product);
        }
        public int GetStock(int id) 
        {
            return _productRepo.GetStock(id);
        }
        public IEnumerable<Product> GetAll()
        {
            return _productRepo.GetAll();
        }
        public CompleteProduct GetById(int id)
        {
            Product p = _productRepo.GetById(id);
            CompleteProduct cp = new CompleteProduct();
            cp.ProductID = p.ProductID;
            cp.Nom = p.Nom;
            cp.Description = p.Description;
            cp.PrixHTVA = p.PrixHTVA;
            cp.Image = p.Image;
            cp.CategorieID= p.CategorieID;
            cp.categorie = _categoriesRepo.GetById(p.CategorieID);
            return cp;
        }
    }
}

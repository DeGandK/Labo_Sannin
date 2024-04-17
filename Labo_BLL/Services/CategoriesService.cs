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
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepo _categorieRepo;
        public CategoriesService(ICategoriesRepo categorieRepo)
        {
            _categorieRepo = categorieRepo;
        }
        public Categories GetById(int id)
        {
            return _categorieRepo.GetById(id);
        }
        public IEnumerable<Categories> GetAll()
        {
            return _categorieRepo.GetAll();
        }
    }
}

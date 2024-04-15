using Labo_Domain.Models;

namespace Labo_BLL.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int id);
        User Login(string email, string password);
        void Register(string nom, string prenom, string email, string password, string telephone, string adresse);
        void Update(User user);
    }
}
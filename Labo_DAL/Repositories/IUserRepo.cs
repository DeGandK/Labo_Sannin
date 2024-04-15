using Labo_Domain.Models;

namespace Labo_DAL.Repositories
{
    public interface IUserRepo
    {
        List<User> GetAll();
        User GetById(int id);
        string GetHashPwd(string email);
        User Login(string email, string password);
        void Register(string nom, string prenom, string email, string password, string telephone, string adresse);
        void Update(User user);
    }
}
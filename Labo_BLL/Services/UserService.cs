using Labo_DAL.Repositories;
using Labo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_BLL.Services
{
    public class UserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public User Login(string email,string password) 
        {
            string VerifyPWD = _userRepo.GetHashPwd(email);
            if (BCrypt.Net.BCrypt.Verify(password, VerifyPWD)) 
            {
                try
                {
                    User connectedUser = _userRepo.Login(email, VerifyPWD);
                    return connectedUser;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                throw new InvalidOperationException("Mots de passe incorrect");
            }
        }
        public void Register(string nom, string prenom, string email,string password,string telephone,string adresse)
        {
            string hashPWD = BCrypt.Net.BCrypt.HashPassword(password);
            try
            {
                _userRepo.Register(nom, prenom, email,hashPWD,telephone,adresse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

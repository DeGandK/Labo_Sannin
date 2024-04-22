using Labo_DAL.Repositories;
using Labo_Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_DAL.Services
{
    public class UserService : IUserRepo
    {
        //private string connectionString;
        //public UserService(IConfiguration config)
        //{
        //    connectionString = config.GetConnectionString("ISTVAN PRIGNOT");
        //}

        private SqlConnection _connection;

        public UserService(SqlConnection conn)
        {
            _connection = conn;
        }

        /// <summary>
        /// Methode pour enregistré un User
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="telephone"></param>
        /// <param name="adresse"></param>
        public void Register(string nom, string prenom, string email, string password, string telephone, string adresse)
        {
            using (SqlConnection cnx = _connection)
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    string sql = "INSERT INTO [User] (Nom, Prenom, Email, MDP, Telephone, Adresse) " +
                        "VALUES (@nom, @prenom, @email, @pwd, @tel, @adresse)";
                    cmd.CommandText = sql;


                    cmd.Parameters.AddWithValue("nom", nom);
                    cmd.Parameters.AddWithValue("prenom", prenom);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("pwd", password);
                    cmd.Parameters.AddWithValue("tel", telephone);
                    cmd.Parameters.AddWithValue("adresse", adresse);

                    cnx.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception ex) { throw ex; }
                    cnx.Close();
                }
            }
        }
        /// <summary>
        /// Méthode pour ce connecter
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public User Login(string email, string password)
        {

            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT UserID, Email, IsAdmin " +
                    "FROM [User] WHERE Email = @email AND MDP = @pwd";

                _connection.Open();

                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("pwd", password);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    
                    if (reader.Read())
                    {
                        return new User
                        {
                            Email = (string)reader["Email"],
                            UserID = (int)reader["UserID"],
                            IsAdmin = (bool)reader["IsAdmin"]
                        };

                    }
                    else throw new InvalidOperationException("Compte utilisateur inexistant");
                }

            }
        }
        /// <summary>
        /// Méthode pour hash le mots de passe
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GetHashPwd(string email)
        {

            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT MDP " +
                    "FROM [User] WHERE Email = @email";

                _connection.Open();

                cmd.Parameters.AddWithValue("email", email);
                string pwd = (string)cmd.ExecuteScalar();
                _connection.Close();
                return pwd;
            }

        }
        private User Converter(SqlDataReader reader)
        {
            return new User
            {
                UserID = (int)reader["UserID"],
                Nom = (string)reader["Nom"],
                Prenom = (string)reader["Prenom"],
                Email = (string)reader["Email"],
                Telephone = (string)reader["Telephone"],
                Adresse = (string)reader["Adresse"],
                IsAdmin = (bool)reader["IsAdmin"]
            };
        }
        /// <summary>
        /// Méthode pour récupéré les utilisateurs
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            List<User> list = new List<User>();
            using (SqlConnection connection = _connection)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [User]";
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(Converter(reader));
                        }
                    }
                    connection.Close();
                }
            }
            return list;
        }
        /// <summary>
        /// Méthode pour récupéré 1 utilisateur
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int UserID)
        {
            User u = new User();
            using (SqlConnection connection = _connection)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [User] WHERE UserID = @id";
                    command.Parameters.AddWithValue("id", UserID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            u = Converter(reader);
                        }
                    }
                    connection.Close();
                }
            }
            return u;
        }
        /// <summary>
        /// Méthode pour mettre a jours les informations
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            using (SqlConnection connection = _connection)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [User] SET Email = @email, Telephone = @tel, Adresse = @adresse WHERE UserID = @id";

                    command.Parameters.AddWithValue("id", user.UserID);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("tel", user.Telephone);
                    command.Parameters.AddWithValue("adresse", user.Adresse);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }



    }
}

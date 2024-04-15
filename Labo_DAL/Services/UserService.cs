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
    public class UserService
    {
        private string connectionString;
        public UserService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("ISTVAN PRIGNOT");
        }
        public void Register(string nom, string prenom, string email,string password,string telephone,string adresse)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    string sql = "INSERT INTO User (Nom, Prenom, Email, MDP, Telephone, Adresse) " +
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
        public User Login(string email, string password)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = "SELECT UserID, Email, IsAdmin " +
                        "FROM User WHERE Email = @email AND Password = @pwd";

                    cnx.Open();

                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("pwd", password);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Email = (string)reader["Email"],
                                UserID = (int)reader["Id"],
                                IsAdmin = (bool)reader["IsAdmin"]
                            };
                        }
                        else throw new InvalidOperationException("Compte utilisateur inexistant");
                    }
                }
            }
        }
        public string GetHashPwd(string email)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = "SELECT Password " +
                        "FROM User WHERE Email = @email";

                    cnx.Open();

                    cmd.Parameters.AddWithValue("email", email);
                    string pwd = (string)cmd.ExecuteScalar();
                    cnx.Close();
                    return pwd;
                }
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
                Adresse = (string)reader["Adresse"]
            };
        }
        public List<User> GetAll()
        {
            List<User> list = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM User";
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

        public User GetById(int id) 
        {
            User u = new User();
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                using (SqlCommand command = connection.CreateCommand()) 
                {
                    command.CommandText = "SELECT * FROM User WHERE Id = @id";
                    command.Parameters.AddWithValue("id", id);
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
        public void Update(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                using (SqlCommand command =connection.CreateCommand()) 
                {
                    command.CommandText = "UPDATE User SET Email = @email, Telephone = @tel, Adresse = @adresse WHERE Id = @id";

                    command.Parameters.AddWithValue("Id", user.UserID);
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

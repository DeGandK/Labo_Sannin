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
    public class CategoriesServices : ICategoriesRepo
    {
        //private string connectionString;

        //public CategoriesServices(IConfiguration config)
        //{
        //    connectionString = config.GetConnectionString("ISTVAN PRIGNOT");
        //}

        private SqlConnection _connection;

        public CategoriesServices(SqlConnection conn)
        {
            _connection = conn;
        }
        private Categories Converter(SqlDataReader reader)
        {
            return new Categories
            {
                CategorieID = (int)reader["CategorieID"],
                Nom = (string)reader["Nom"],
                Description = (string)reader["Description"],
                TauxTVA = (int)reader["TauxTVA"]
            };
        }
        /// <summary>
        /// Méthode pour récupérer une catégorie en particulier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Categories GetById(int id)
        {
            Categories categories = new Categories();
            using (SqlConnection connection = _connection)
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Categories WHERE CategorieID = @CategorieID";
                    cmd.Parameters.AddWithValue("CategorieID", id);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            categories = (Converter(reader));
                        }
                    }
                    connection.Close();
                }
            }
            return categories;
        }
        /// <summary>
        /// Méthode pour retourner la liste des catégories
        /// </summary>
        /// <returns></returns>
        public List<Categories> GetAll()
        {
            List<Categories> categories = new List<Categories>();
            using (SqlConnection connection = _connection)
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Categories";
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(Converter(reader));
                        }
                    }
                    connection.Close();
                }
            }
            return categories;
        }
    }
}

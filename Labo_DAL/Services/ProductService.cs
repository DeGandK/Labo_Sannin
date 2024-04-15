using Labo_Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_DAL.Services
{
    public class ProductService
    {
        private string connectionString;
        
        public ProductService (IConfiguration config)
        {
           connectionString = config.GetConnectionString("default");
        }

        //CRUD
        /// <summary>
        /// Cette méthode créer un produit et renvoie l'Id dont le produit a hérité
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int Create(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO Product (Nom,Description,Stock,PrixHTVA,Image,CategorieID) VALUES " +
                        "(@Nom,@Description,@Stock,@PrixHTVA,@Image,@CategorieID)";

                    command.Parameters.AddWithValue("Nom", product.Nom);
                    command.Parameters.AddWithValue("Description", product.Description);
                    command.Parameters.AddWithValue("Stock", product.Stock);
                    command.Parameters.AddWithValue("PrixHTVA", product.PrixHTVA);
                    command.Parameters.AddWithValue("Image", product.Image);
                    command.Parameters.AddWithValue("CategorieID", product.CategorieID);

                    try
                    {
                        connection.Open();
                        int createdId = (int)command.ExecuteScalar();
                        connection.Close();
                        return createdId;
                    }
                    catch (SqlException ex)
                    {
                        //Gérer l'exception
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public Product GetById(int id)
        {
            Product p = new Product();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM Product WHERE Id = @ProductId";

                    command.Parameters.AddWithValue ("id", id);

                }
            }
        }


    }
}

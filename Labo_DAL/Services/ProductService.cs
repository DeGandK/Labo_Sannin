using Labo_DAL.Repositories;
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
    public class ProductService : IProductRepo
    {
        private string connectionString;

        public ProductService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("KEVIN DE GAND");
        }

        private Product Converter(SqlDataReader reader)
        {
            return new Product
            {
                ProductID = (int)reader["ProductID"],
                Nom = (string)reader["Nom"],
                Description = (string)reader["Description"],
                Image = (string)reader["Image"],
                PrixHTVA = (decimal)reader["PrixHTVA"],
                Stock = (int)reader["Stock"],
                CategorieID = (int)reader["CategorieID"]
            };
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
        /// <summary>
        /// Cette méthode retourne un produit en prenant son id en paramètre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetById(int id)
        {
            Product p = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM Product WHERE Id = @ProductId";

                    command.Parameters.AddWithValue("id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            p = Converter(reader);
                        }
                    }
                    connection.Close();
                }
            }
            return p;
        }
        /// <summary>
        /// Cette méthode retourne une liste de tous les produits
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAll()
        {
            List<Product> listeProduit = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM Product";
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listeProduit.Add(Converter(reader));

                        }
                    }
                    connection.Close();
                }
            }
            return listeProduit;
        }
        /// <summary>
        /// Cette méthode supprimer un produit en prenant son id en paramètre
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM Product WHERE ProductID = @Id";

                    command.Parameters.AddWithValue("ID", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// Cette méthode prend un produit en paramètre et permet de modifier ces données
        /// </summary>
        /// <param name="product"></param>
        public void Update(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(connectionString))
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE Product SET Nom = @Nom, Description = @Description, Stock = @Stock, PrixHTVA = @PrixHTVA, Image = @Image, CategorieID = @CategorieID WHERE ProductID = @Id";

                    command.Parameters.AddWithValue("Nom", product.Nom);
                    command.Parameters.AddWithValue("Description", product.Description);
                    command.Parameters.AddWithValue("Stock", product.Stock);
                    command.Parameters.AddWithValue("PrixHTVA", product.PrixHTVA);
                    command.Parameters.AddWithValue("Image", product.Image);
                    command.Parameters.AddWithValue("CategorieID", product.CategorieID);
                }
            }
        }
    }
}

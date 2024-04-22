﻿using Labo_DAL.Repositories;
using Labo_Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_DAL.Services
{
    public class ProductService : IProductRepo
    {
        //private string connectionString;

        //public ProductService(IConfiguration config)
        //{
        //    connectionString = config.GetConnectionString("ISTVAN PRIGNOT");
        //}

        private SqlConnection _connection;

        public ProductService(SqlConnection conn)
        {
            _connection = conn;
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
        public void Create(Product product)
        {
            using (SqlConnection connection = _connection)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
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
                        command.ExecuteNonQuery();
                        connection.Close();
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
            using (SqlConnection connection = _connection)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Product WHERE ProductID = @ProductId";

                    command.Parameters.AddWithValue("ProductID", id);
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
        public int GetStock(int id)
        {
            int stock;
            using (SqlConnection connection = _connection)
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Stock FROM Product WHERE ProductID = @ProductId";
                    cmd.Parameters.AddWithValue("ProductID", id);
                    connection.Open();
                    stock = (int)cmd.ExecuteScalar();
                    connection.Close();
                }
            }
            return stock;
        }

        /// <summary>
        /// Cette méthode retourne une liste de tous les produits
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAll()
        {
            List<Product> listeProduit = new List<Product>();
            using (SqlConnection connection = _connection)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
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
            using (SqlConnection connection = _connection)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Product WHERE ProductID = @Id";

                    command.Parameters.AddWithValue("Id", id);

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
            using (SqlConnection connection = _connection)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Product SET Nom = @Nom, Description = @Description, Stock = @Stock, PrixHTVA = @PrixHTVA, Image = @Image, CategorieID = @CategorieID WHERE ProductID = @Id";

                    command.Parameters.AddWithValue("Id", product.ProductID);
                    command.Parameters.AddWithValue("Nom", product.Nom);
                    command.Parameters.AddWithValue("Description", product.Description);
                    command.Parameters.AddWithValue("Stock", product.Stock);
                    command.Parameters.AddWithValue("PrixHTVA", product.PrixHTVA);
                    command.Parameters.AddWithValue("Image", product.Image);
                    command.Parameters.AddWithValue("CategorieID", product.CategorieID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

    }
}

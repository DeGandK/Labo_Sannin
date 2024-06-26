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
                CategorieID = (int)reader["CategorieID"],
                IsActif = (bool)reader["IsActif"]
            };
        }

        //CRUD
        /// <summary>
        /// Cette méthode crée un produit et renvoie l'Id dont le produit a hérité
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public void Create(Product product)
        {

            using (SqlCommand command = _connection.CreateCommand())
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
                    _connection.Open();
                    command.ExecuteNonQuery();
                    _connection.Close();
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
        /// <summary>
        /// Cette méthode retourne un produit en prenant son id en paramètre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetById(int id)
        {
            Product p = new Product();

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Product WHERE ProductID = @ProductId";

                command.Parameters.AddWithValue("ProductID", id);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        p = Converter(reader);
                    }
                }
                _connection.Close();
            }

            return p;
        }
        /// <summary>
        /// Cette méthode retourne la quantité en stock d'un produit en prenant son id en paramètre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetStock(int id)
        {
            int stock = 0;

            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT Stock FROM Product WHERE ProductID = @ProductID";
                cmd.Parameters.AddWithValue("ProductID", id);
                _connection.Open();
                stock = (int)cmd.ExecuteScalar();
                _connection.Close();
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

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Product";
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listeProduit.Add(Converter(reader));

                    }
                }
                _connection.Close();

            }
            return listeProduit;
        }
        /// <summary>
        /// Cette méthode supprime un produit en prenant son id en paramètre
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Product WHERE ProductID = @Id";

                command.Parameters.AddWithValue("Id", id);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }
        /// <summary>
        /// Cette méthode prend un produit en paramètre et permet de modifier ses données
        /// </summary>
        /// <param name="product"></param>
        public void Update(Product product)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Product SET Nom = @Nom, Description = @Description, Stock = @Stock, PrixHTVA = @PrixHTVA, Image = @Image, CategorieID = @CategorieID WHERE ProductID = @Id";

                command.Parameters.AddWithValue("Id", product.ProductID);
                command.Parameters.AddWithValue("Nom", product.Nom);
                command.Parameters.AddWithValue("Description", product.Description);
                command.Parameters.AddWithValue("Stock", product.Stock);
                command.Parameters.AddWithValue("PrixHTVA", product.PrixHTVA);
                command.Parameters.AddWithValue("Image", product.Image);
                command.Parameters.AddWithValue("CategorieID", product.CategorieID);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}

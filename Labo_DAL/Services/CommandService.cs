using Labo_DAL.Repositories;
using Labo_Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Labo_DAL.Services
{
    public class CommandService : ICommandRepo
    {
        //private string connectionString;

        //public CommandService(IConfiguration config)
        //{
        //    connectionString = config.GetConnectionString("ISTVAN PRIGNOT");
        //}

        private SqlConnection _connection;

        public CommandService(SqlConnection conn)
        {
            _connection = conn;
        }
        private Command Converter(SqlDataReader reader)
        {
            return new Command
            {
                CommandID = (int)reader["CommandID"],
                UserID = (int)reader["UserID"],
                IsPaid = (bool)reader["IsPaid"],
                DateCommande = (DateTime)reader["DateCommande"]
            };
        }
        /// <summary>
        /// Cette méthode retourne une liste de tous les commandes
        /// </summary>
        /// <returns></returns>
        public List<Command> GetAll()
        {
            List<Command> list = new List<Command>();

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Command";
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(Converter(reader));
                    }
                }
                _connection.Close();
            }

            return list;
        }
        //CRUD
        /// <summary>
        /// Cette méthode créer une commande et renvoie l'Id dont la commande a hérité
        /// </summary>
        /// <param name="Command"></param>
        /// <returns></returns>
        public int Create(Command cs)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Command (UserID, IsPaid, DateCommande) output inserted.CommandID " +
                        "VALUES (@UID, @Paid, @DCmd)";

                cmd.Parameters.AddWithValue("UID", cs.UserID);
                cmd.Parameters.AddWithValue("Paid", cs.IsPaid);
                cmd.Parameters.AddWithValue("DCmd", cs.DateCommande);
                _connection.Open();
                int id = (int)cmd.ExecuteScalar();
                _connection.Close();
                return id;
            }
        }
        public List<Command> GetCommandsbyUserID(int UserID)
        {
            List<Command> list = new List<Command>();
            using (SqlConnection connection = _connection)
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Command WHERE UserID = @UserID";
                    cmd.Parameters.AddWithValue("UserID", UserID);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
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
        /// Cette méthode sert à Valider une commande si celle ci a bien été payée
        /// </summary>
        /// <param name="CommandId"></param>
        public void ValiderCommande(int CommandId)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE Command SET IsPaid = 1 WHERE CommandId = @CommandId";
                cmd.Parameters.AddWithValue("CommandId", CommandId);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        /// <summary>
        /// Cette méthode sert à supprimer la commande si elle n'a pas été payée
        /// </summary>
        /// <param name="commandId"></param>
        public void DeleteCommande(int CommandId)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "DELETE  FROM Command WHERE CommandID = @CommandId";
                cmd.Parameters.AddWithValue("CommandId", CommandId);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}

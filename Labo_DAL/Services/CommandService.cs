using Labo_DAL.Repositories;
using Labo_Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Labo_DAL.Services
{
    public class CommandService : ICommandRepo
    {
        private string connectionString;

        public CommandService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("ISTVAN PRIGNOT");
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
            using (SqlConnection connection = new SqlConnection())
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Command";
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
        //CRUD
        /// <summary>
        /// Cette méthode créer une commande et renvoie l'Id dont la commande a hérité
        /// </summary>
        /// <param name="Command"></param>
        /// <returns></returns>
        public void Creat(Command cs)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Command (CommandID, UserID, IsPaid, DateCommande) " +
                            "VALUES (@CmdID, @UID, @Paid, @DCmd)";

                    cmd.Parameters.AddWithValue("CmdID", cs.CommandID);
                    cmd.Parameters.AddWithValue("UID", cs.UserID);
                    cmd.Parameters.AddWithValue("Paid", cs.IsPaid);
                    cmd.Parameters.AddWithValue("DCmd", cs.DateCommande);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public List<Command> GetCommandsbyUserID(int UserID)
        {
            List<Command> list = new List<Command>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Command c JOIN CommandRow cr ON c.ID = cr.CommandID WHERE cr.UserID = @id";
                    cmd.Parameters.AddWithValue("id", UserID);
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
    }
}

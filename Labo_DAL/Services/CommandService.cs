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
    public class CommandService
    {
        private string connectionString;

        public CommandService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("default");
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
        public List<CommandService> GetAll()
        {
            List<CommandService> list = new List<CommandService>();
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
        public void Creat(CommandService cs)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Command (CommandID, UserID, IsPaid, DateCommande) " +
                        "VALUES (@CmdID, @UID, @Paid, @DCmd)";

                cmd.Parameters.AddWithValue("CmdID", cs.CommandID);
                cmd.Parameters.AddWithValue("UID", cs.UserID);
                cmd.Parameters.AddWithValue("Paid", cs.IsPaid);
                cmd.Parameters.AddWithValue("DCmd", cs.DateCommade);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

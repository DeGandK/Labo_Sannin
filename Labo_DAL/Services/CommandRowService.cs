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
    public class CommandRowService : ICommandRowRepo
    {
        //private string connectionString;

        //public CommandRowService(IConfiguration config)
        //{
        //    connectionString = config.GetConnectionString("ISTVAN PRIGNOT");
        //}

        private SqlConnection _connection;

        public CommandRowService(SqlConnection conn)
        {
            _connection = conn;
        }
        private CommandRow Converter(SqlDataReader reader)
        {
            return new CommandRow
            {
                CommandID = (int)reader["CommandID"],
                ProductID = (int)reader["ProductID"],
                Quantite = (int)reader["Quantite"]
            };
        }
        public void Create(CommandRow cr) 
        {
            using (SqlConnection cnx = _connection) 
            {
                using (SqlCommand cmd = cnx.CreateCommand()) 
                {
                    string sql = "INSERT INTO CommandRow (CommandID,ProductID,Quantite) VALUES (@CID,@PID,@Quant)";
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("CID", cr.CommandID);
                    cmd.Parameters.AddWithValue("PID", cr.ProductID);
                    cmd.Parameters.AddWithValue("Quant", cr.Quantite);
                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                }
            }
        }
        public List<CommandRow> GetByCommandId(int id)
        {
            List<CommandRow> commandRows = new List<CommandRow>();
            using (SqlConnection cnx = _connection)
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    string sql = "SELECT Stock FROM Product WHERE ProductID = ProductID";
                    cmd.CommandText = sql;
                    cnx.Open();
                    int stock = (int)cmd.ExecuteScalar();
                    cnx.Close();

                    if (stock > 0 )
                    {
                        cmd.CommandText = "SELECT * FROM Product p JOIN CommandRow cr ON p.ProductID = cr.ProductID WHERE CommandID = @CommandID";
                        cmd.Parameters.AddWithValue("CommandID", id);
                        cnx.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                commandRows.Add(Converter(reader));
                            }
                        }
                        cnx.Close();
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
            return commandRows;
        }






    }
}

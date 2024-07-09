using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using tcit.Models;
using tcit.Repository;
using Npgsql;

namespace tcit.DataAccesLayer
{
    public class ItemDal : IItemService
    {
        private string connectionString;

        public ItemDal()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MyDbContext"].ConnectionString;
        }

        public List<Item> GetAllItems(string nombre = null)
        {
            List<Item> itemList = new List<Item>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT Id, Nombre, Descripcion FROM Items";
                if (!string.IsNullOrEmpty(nombre))
                {
                    query += " WHERE Nombre LIKE @Nombre";
                }

                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                if (!string.IsNullOrEmpty(nombre))
                {
                    cmd.Parameters.AddWithValue("@Nombre", $"%{nombre}%");
                }

                cmd.CommandType = CommandType.Text;
                con.Open();
                NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Item item = new Item
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Nombre = rdr["Nombre"].ToString(),
                        Descripcion = rdr["Descripcion"].ToString()
                    };

                    itemList.Add(item);
                }
            }

            return itemList;
        }

        public bool AddItem(Item item)
        {
            try
            {
                using (var con = new NpgsqlConnection(connectionString))
                {
                    var cmd = new NpgsqlCommand("INSERT INTO Items (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)", con);
                    cmd.Parameters.AddWithValue("@Nombre", item.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", item.Descripcion);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteItem(int id)
        {
            try
            {
                using (var con = new NpgsqlConnection(connectionString))
                {
                    var cmd = new NpgsqlCommand("DELETE FROM Items WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
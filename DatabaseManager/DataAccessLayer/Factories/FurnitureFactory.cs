using DatabaseManager.DataAccessLayer.Factories.Helpers;
using DatabaseManager.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.DataAccessLayer.Factories
{
    public class FurnitureFactory
    {
        public List<Furniture> All()
        {
            List<Furniture> furnitures = new List<Furniture>();
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.AllFurnitures;

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    furnitures.Add(CreateFromReader.Furniture(reader));
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection?.Close();
            }

            return furnitures;
        }

        public List<Furniture> ByRoom(int room)
        {
            List<Furniture> furnitures = new List<Furniture>();
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.AllFurnituresByRoom;
                command.Parameters.AddWithValue("@Room", room);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    furnitures.Add(CreateFromReader.Furniture(reader));
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection?.Close();
            }

            return furnitures;
        }

        public Furniture ById(int id)
        {
            Furniture furniture = new Furniture();
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.FurnitureById;
                command.Parameters.AddWithValue("@Id", id);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    furniture = CreateFromReader.Furniture(reader);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection?.Close();
            }

            return furniture;
        }

        public void Create(Furniture item)
        {
            MySqlConnection? connection = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.CreateFurniture;
                command.Parameters.AddWithValue("@Room", item.Room_Id);
                command.Parameters.AddWithValue("@Brand", item.Brand);
                command.Parameters.AddWithValue("@Type", item.Type);
                command.Parameters.AddWithValue("@Description", item.Description);
                command.Parameters.AddWithValue("@Length", item.Length);
                command.Parameters.AddWithValue("@Height", item.Height);
                command.Parameters.AddWithValue("@Width", item.Width);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection?.Close();
            }
        }

        public void Update(Furniture item)
        {
            MySqlConnection? connection = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.UpdateFurniture;
                command.Parameters.AddWithValue("@Room", item.Room_Id);
                command.Parameters.AddWithValue("@Brand", item.Brand);
                command.Parameters.AddWithValue("@Type", item.Type);
                command.Parameters.AddWithValue("@Description", item.Description);
                command.Parameters.AddWithValue("@Length", item.Length);
                command.Parameters.AddWithValue("@Height", item.Height);
                command.Parameters.AddWithValue("@Width", item.Width);
                command.Parameters.AddWithValue("@Id", item.Id);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection?.Close();
            }
        }

        public void Delete(int id)
        {
            MySqlConnection? connection = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.DeleteFurniture;
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}

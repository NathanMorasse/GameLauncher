using HourGlassUnlimited.DataAccessLayer.Factories.Base;
using HourGlassUnlimited.DataAccessLayer.Factories.Helper;
using HourGlassUnlimited.Exceptions;
using HourGlassUnlimited.Models;
using HourGlassUnlimited.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.DataAccessLayer.Factories
{
    public class GameFactory : FactoryBase
    {
        public static GameBase CreateFromReader(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string title = reader["Title"].ToString() ?? string.Empty;

            return new GameBase() { Id = id, Title = title };
        }

        public GameBase GetByTitle(string title)
        {
            GameBase? game = null;
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM games WHERE Title=@Title;";
                command.Parameters.AddWithValue("@Title", title);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    game = CreateFromReader(reader);
                }

                if (game == null)
                {
                    throw new ObjectNotFoundException("Le jeu avec le titre: "+title+" n'existe pas");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Échec du chargement du jeu avec le titre: " + title, e.InnerException);
            }
            finally
            {
                connection?.Close();
            }
            return game;
        }

        public GameBase GetById(int id)
        {
            GameBase? game = null;
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM games WHERE Id=@Id;";
                command.Parameters.AddWithValue("@Id", id);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    game = CreateFromReader(reader);
                }

                if (game == null)
                {
                    throw new ObjectNotFoundException("Le jeu avec l'ID "+id+" n'existe pas");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Échec du chargement du jeu avec l'ID: " + id, e.InnerException);
            }
            finally
            {
                connection?.Close();
            }
            return game;
        }
    }
}

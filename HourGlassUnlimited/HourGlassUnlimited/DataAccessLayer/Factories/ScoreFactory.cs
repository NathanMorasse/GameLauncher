using HourGlassUnlimited.DataAccessLayer.Factories.Base;
using HourGlassUnlimited.Models;
using HourGlassUnlimited.Games.Sudoku.Tools;
using HourGlassUnlimited.Tools;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.DataAccessLayer.Factories
{
    public class ScoreFactory : FactoryBase
    {
        public async void SaveScore(Score newScore)
        {
            MySqlConnection? connection = null;
            try
            {
                connection = new MySqlConnection(ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO scores(User, Game, Category, Time, Date) VALUES(@User, @Game, @Category, @Time, @Date);";
                command.Parameters.AddWithValue("@User", newScore.UserId);
                command.Parameters.AddWithValue("@Game", newScore.GameId);
                command.Parameters.AddWithValue("@Category", newScore.Category);
                command.Parameters.AddWithValue("@Time", newScore.Time);
                command.Parameters.AddWithValue("@Date", newScore.Date);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Echec de l'enregistrement: " + e.Message);
            }
        }
    }
}

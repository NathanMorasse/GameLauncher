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
using System.Windows.Documents;
using HourGlassUnlimited.DataAccessLayer.Factories.Helper;

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
                command.CommandText = "insert into scores(`User`, `Game`, `Category`, `Time`, `Date`) values((select `Id` from `users` where `Username` = @User), 1, @Category, @Time, @Date);";
                command.Parameters.AddWithValue("@User", newScore.User);
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

        public List<Score> CustomList(int? user, string? game, string cmd)
        {
            List<Score>? scores = new List<Score>();
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = cmd;
                if (user != null)
                    command.Parameters.AddWithValue("@User", user);
                if (game != null)
                    command.Parameters.AddWithValue("@Game", game);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var score = FactoryHelper.ScoreFromReader(reader);
                    scores.Add(score);
                }

                if (scores.Count == 0)
                {
                    var score = new Score(0, string.Empty, 0, "NotFound", "Aucun score n'a été trouvé.", TimeSpan.Zero, 0, DateTime.Now);
                    scores.Add(score);
                }
            }
            catch (Exception e)
            {
                var score = new Score(-1, string.Empty, -1, "Error", e.Message, TimeSpan.Zero, -1, DateTime.Now);
                scores.Add(score);
            }

            return scores;
        }

        public bool UserCompletedDaily(int gameId)
        {
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;
            bool isCompleted = false;

            try
            {
                connection = new MySqlConnection(ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbdev.scores Where User=@User AND Game=@Game AND DATE(Date)=@Date AND Category='Daily';";
                command.Parameters.AddWithValue("@User", ConnectionHelper.User.Id);
                command.Parameters.AddWithValue("@Game", gameId);
                command.Parameters.AddWithValue("@Date", DateTime.Now.Date);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    isCompleted = !reader.IsDBNull(1);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erreur de validation du score du joueur " + ConnectionHelper.User.Username+" :"+e.Message);
            }
            
            return isCompleted;
        }
    }
}
